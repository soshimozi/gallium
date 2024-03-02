namespace Gallium.Types;

public class MethodInfo
{
    public string MethodName { get; set; }
    public TypeInfo Type { get; set; }
    public List<SymbolInfo> Parameters { get; set; }
    public bool IsPrivate { get; set; }
    public string ClassName { get; set; }

    public MethodInfo(string className, string methodName, TypeInfo type, List<SymbolInfo> parameters, bool isPrivate)
    {
        ClassName = className;
        MethodName = methodName;
        Type = type;
        Parameters = parameters;
        IsPrivate = isPrivate;
    }

}