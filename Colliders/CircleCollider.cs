using System.Numerics;
using Box2DX.Dynamics;

namespace Engine.Physics.Box2D.Colliders;

public class CircleCollider : Box2DAbstractCollider
{
    public float Radius;
    public Vector2 Offset;

    public override Fixture GetFixture(Body body)
    {
        var def = new CircleDef
        {
            Radius = Radius,
            Density = Density,
            Friction = Friction,
            Restitution = Restitution,
            IsSensor = IsTrigger,
            LocalPosition = Offset.ToBox2DVec2()
        };
        return body.CreateFixture(def);
    }
}
