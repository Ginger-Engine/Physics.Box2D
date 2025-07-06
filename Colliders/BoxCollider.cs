using System.Numerics;
using Box2DX.Common;
using Box2DX.Dynamics;

namespace Engine.Physics.Box2D.Colliders;

public class BoxCollider : Box2DAbstractCollider
{
    public Vector2 Size;
    public Vector2 Offset;
    public override Fixture GetFixture(Body body)
    {
        var def = new PolygonDef
        {
            Density = Density,
            Friction = Friction,
            Restitution = Restitution,
            IsSensor = IsTrigger,
        };
        def.SetAsBox(Size.X / 2, Size.Y / 2, new Vec2(Size.X / 2, Size.Y / 2), 0);
        return body.CreateFixture(def);
    }
}