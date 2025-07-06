using Engine.Core.Stages;

namespace Engine.Physics.Box2D;

public class PhysicsStage(IPhysicsWorld physicsWorld) : IStage
{
    public Type[] Before { get; set; }
    public Type[] After { get; set; } = [typeof(LogicStage)];
    
    private float _accumulator = 0f;
    private const float _fixedDelta = 1f / 60f;
    
    public void Start()
    {
        
    }

    public void Update(float dt)
    {
        _accumulator += dt;

        while (_accumulator >= _fixedDelta)
        {
            physicsWorld.Step(_fixedDelta);
            _accumulator -= _fixedDelta;
        }
    }
}