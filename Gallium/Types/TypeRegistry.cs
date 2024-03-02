namespace Gallium.Types;

public class TypeRegistry
{
    private Dictionary<string, TypeInfo> _types = new Dictionary<string, TypeInfo>();

    public void AddType(string name, TypeInfo type)
    {
        _types[name] = type;
    }

    public TypeInfo? GetType(string name)
    {
        return _types.TryGetValue(name, out var typeInfo) ? typeInfo : null;
    }
}