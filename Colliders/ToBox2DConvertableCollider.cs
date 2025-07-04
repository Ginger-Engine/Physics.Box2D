using Box2DX.Dynamics;

namespace Engine.Physics.Box2D.Colliders;

public interface ToBox2DConvertableCollider
{
    public Fixture GetFixture(Body body);
}