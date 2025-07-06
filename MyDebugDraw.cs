using System.Numerics;
using Box2DX.Common;
using Box2DX.Dynamics;
using Engine.Rendering;

namespace Engine.Physics.Box2D;

public class MyDebugDraw : DebugDraw
{
    private readonly List<(Vector2 from, Vector2 to, System.Drawing.Color color)> _lines = [];
    
    public bool IsRendered;

    public MyDebugDraw(RenderingStage renderingStage)
    {
        renderingStage.OnAfterRenderEvent += backend =>
        {
            foreach (var (from, to, color) in _lines)
                backend.DrawLine(from, to, color, 3);
            IsRendered = true;
        };
    }
    public override void DrawPolygon(Vec2[] vertices, int vertexCount, Color color)
    {
        if (IsRendered)
        {
            _lines.Clear();
            IsRendered = false;
        }
        for (int i = 0; i < vertexCount; ++i)
        {
            var p1 = vertices[i].ToVector2();
            var p2 = vertices[(i + 1) % vertexCount].ToVector2();
            var c = System.Drawing.Color.FromArgb(255, (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
            _lines.Add((p1, p2, c));
        }
    }

    public override void DrawSolidPolygon(Vec2[] vertices, int vertexCount, Color color) => DrawPolygon(vertices, vertexCount, color);
    public override void DrawCircle(Vec2 center, float radius, Color color) { }
    public override void DrawSolidCircle(Vec2 center, float radius, Vec2 axis, Color color) { }
    public override void DrawSegment(Vec2 p1, Vec2 p2, Color color) { }
    public override void DrawXForm(XForm xf) { }
}
