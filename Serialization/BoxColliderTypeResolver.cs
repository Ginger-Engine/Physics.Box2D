using System.Globalization;
using System.Numerics;
using Engine.Physics.Box2D.Colliders;
using Engine.Physics.Serialization;

namespace Engine.Physics.Box2D.Serialization;

public class BoxColliderTypeResolver : ISpecificColliderResolver
{
    public Type Type { get; } = typeof(BoxCollider);

    public object Resolve(object raw)
    {
        if (raw is not Dictionary<object, object> dict)
            throw new ArgumentException("Expected a dictionary for BoxCollider deserialization");

        Vector2 ParseVector2(object? value)
        {
            if (value is Dictionary<object, object> vecDict &&
                vecDict.TryGetValue("x", out var x) &&
                vecDict.TryGetValue("y", out var y))
            {
                return new Vector2(
                    float.Parse(x.ToString(), CultureInfo.InvariantCulture), 
                    float.Parse(y.ToString(), CultureInfo.InvariantCulture)
                    );
            }

            throw new ArgumentException("Invalid Vector2 format");
        }
        
        if (dict["parameters"] is not Dictionary<object, object> parameters)
            throw new ArgumentException("Expected a dictionary for BoxCollider deserialization");
        
        var collider = new BoxCollider
        {
            Size = parameters.TryGetValue("Size", out var sizeVal) ? ParseVector2(sizeVal) : Vector2.One,
            Offset = parameters.TryGetValue("Offset", out var offsetVal) ? ParseVector2(offsetVal) : Vector2.Zero,
            Density = parameters.TryGetValue("Density", out var densityVal) ? float.Parse(densityVal.ToString(), CultureInfo.InvariantCulture) : 1f,
            Friction = parameters.TryGetValue("Friction", out var frictionVal) ? float.Parse(frictionVal.ToString(), CultureInfo.InvariantCulture) : 0.2f,
            Restitution = parameters.TryGetValue("Restitution", out var restitutionVal) ? float.Parse(restitutionVal.ToString(), CultureInfo.InvariantCulture) : 0f,
            IsTrigger = parameters.TryGetValue("IsTrigger", out var isTriggerVal) && Convert.ToBoolean(isTriggerVal)
        };

        return collider;
    }
}
