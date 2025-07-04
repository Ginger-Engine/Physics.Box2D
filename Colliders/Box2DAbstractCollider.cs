using Box2DX.Dynamics;
using Engine.Physics.Colliders;

namespace Engine.Physics.Box2D.Colliders;

public abstract class Box2DAbstractCollider : AbstractCollider, ToBox2DConvertableCollider
{
    public abstract Fixture GetFixture(Body body);
}