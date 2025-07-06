using Engine.Core;
using Engine.Physics.Box2D.Serialization;
using GignerEngine.DiContainer;

namespace Engine.Physics.Box2D;

public class PhysicsBox2DBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<IPhysicsWorld>().From<Box2DPhysicsWorld>();
        builder.Bind<PhysicsStage>();
        builder.Bind<BoxColliderTypeResolver>();
    }
}