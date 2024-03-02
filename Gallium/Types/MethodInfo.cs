namespace Gallium.Types;

public class MethodInfo
{
    public string Name { get; set; }
    public TypeInfo Type { get; set; }
    public List<SymbolInfo> Parameters { get; set; }
    public bool IsPrivate { get; set; }

    public MethodInfo(string name, TypeInfo type, List<SymbolInfo> parameters, bool isPrivate)
    {
        Name = name;
        Type = type;
        Parameters = parameters;
        IsPrivate = isPrivate;
    }

}