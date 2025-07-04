using System.Numerics;
using Box2DX.Common;
using Box2DX.Dynamics;

namespace Engine.Physics.Box2D;

internal static class ConversionsExtension
{
    internal static Vector2 ToVector2(this Vec2 v) => new(v.X, v.Y);
    internal static Vec2 ToBox2DVec2(this Vector2 v) => new(v.X, v.Y);

    internal static BodyDef ToBox2DBodyDef(this BodyDefinition bodyDefinition)
    {
        var massData = new Box2DX.Collision.MassData();
        massData.Center.SetZero();
        massData.Mass = 0.0f;
        massData.I = 0.0f;
        
        return new BodyDef
        {
            MassData = massData,
            UserData = null,
            Position = bodyDefinition.Position.ToBox2DVec2(),
            Angle = bodyDefinition.Rotation,
            LinearVelocity = bodyDefinition.LinearVelocity.ToBox2DVec2(),
            AngularVelocity = bodyDefinition.AngularVelocity,
            LinearDamping = bodyDefinition.LinearDamping,
            AngularDamping = bodyDefinition.AngularDamping,
            AllowSleep = bodyDefinition.AllowSleep,
            IsSleeping = bodyDefinition.IsSleeping,
            FixedRotation = bodyDefinition.FixedRotation,
            IsBullet = bodyDefinition.IsBullet,
        };
    }
}