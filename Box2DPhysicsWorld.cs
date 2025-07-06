using System.Numerics;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using Engine.Physics.Box2D.Colliders;
using Engine.Physics.Colliders;
using Engine.Physics.Components;
using Engine.Rendering;

namespace Engine.Physics.Box2D;

public class Box2DPhysicsWorld : IPhysicsWorld
{
    private readonly World world;

    public Box2DPhysicsWorld(RenderingStage renderingStage)
    {
        var aabb = new AABB
        {
            LowerBound = new Vec2(-1000, -1000),
            UpperBound = new Vec2(1000, 1000)
        };
        world = new World(aabb, new Vec2(0, 0), true);
        var debugDraw = new MyDebugDraw(renderingStage);
        debugDraw.AppendFlags(DebugDraw.DrawFlags.Shape);
        debugDraw.AppendFlags(DebugDraw.DrawFlags.CoreShape);
        // debugDraw.AppendFlags(DebugDraw.DrawFlags.CenterOfMass);
        world.SetDebugDraw(debugDraw);
    }

    public Vector2 Gravity
    {
        get => world.Gravity.ToVector2();
        set => world.Gravity = value.ToBox2DVec2();
    }

    public IPhysicsBody CreateBody(BodyDefinition bodyDefinition, ICollider[] colliders)
    {
        var body = world.CreateBody(bodyDefinition.ToBox2DBodyDef());
        foreach (var collider in colliders)
        {
            if (collider is Box2DAbstractCollider box2DCollider)
            {
                box2DCollider.GetFixture(body);
            }
            else throw new Exception("Collider type is not Box2DAbstractCollider");
        }

        body.SetMassFromShapes();
        if (bodyDefinition.BodyType != PhysicsBodyType.Dynamic)
            body.SetStatic();
        return new Box2DPhysicsBody(body);
    }

    public void Step(float dt)
    {
        const int velocityIterations = 8;
        const int positionIterations = 3;

        world.Step(dt, velocityIterations, positionIterations);
    }

    public void DestroyBody(IPhysicsBody body)
    {
        if (body is Box2DPhysicsBody box2dBody)
            world.DestroyBody(box2dBody.Body);
        else
            throw new InvalidOperationException("Body is not Box2DPhysicsBody");
    }
}