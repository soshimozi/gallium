namespace Gallium.Types;

public class MethodInfo
{
    public string MethodName { get; set; }
    public TypeInfo Type { get; set; }
    public List<SymbolInfo> Parameters { get; set; }
    public bool IsPrivate { get; set; }
    public TypeInfo ContainerType { get; set; }

    public MethodInfo(TypeInfo typeInfo, string methodName, TypeInfo type, List<SymbolInfo> parameters, bool isPrivate)
    {
        ContainerType = typeInfo;
        MethodName = methodName;
        Type = type;
        Parameters = parameters;
        IsPrivate = isPrivate;
    }

}