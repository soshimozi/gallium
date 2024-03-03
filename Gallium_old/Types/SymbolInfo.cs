namespace Gallium.Types;

public class SymbolInfo
{
    public string Name { get; set; } = string.Empty;
    public TypeInfo Type { get; set; }
    public object? Value { get; set; } = null;

    public SymbolInfo(string name, TypeInfo type)
    {
        Name = name;
        Type = type;
    }
}

