using System.Numerics;
using Box2DX.Common;
using Box2DX.Dynamics;

namespace Engine.Physics.Box2D;

public class Box2DPhysicsBody(Body body) : IPhysicsBody
{
    public Body Body => body;
    public Vector2 Position
    {
        get => body.GetPosition().ToVector2();
        set => body.SetPosition(value.ToBox2DVec2());
    }
    
    public float Rotation
    {
        get => body.GetAngle();
        set => body.SetAngle(value);
    }

    public void ApplyForce(Vector2 force) =>
        body.ApplyForce(new Vec2(force.X, force.Y), body.GetPosition());

    public void ApplyLinearImpulse(Vector2 impulse) =>
        body.ApplyImpulse(impulse.ToBox2DVec2(), body.GetPosition());

    public Vector2 GetLinearVelocity() => body.GetLinearVelocity().ToVector2();

    public void SetLinearVelocity(Vector2 velocity) =>
        body.SetLinearVelocity(velocity.ToBox2DVec2());

    public void SetFixedRotation(bool isFixed) =>
        body.SetFixedRotation(isFixed);
}
