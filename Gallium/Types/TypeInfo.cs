namespace Gallium.Types;

public class TypeInfo
{
    public string Name { get; set; }
    public Dictionary<string, GType> Fields { get; set; }
    public Dictionary<string, MethodInfo> Methods { get; set; }
    public List<ConstructorInfo> Constructors { get; set; }

    public TypeInfo(string typeName)
    {
        Name = typeName;
        Constructors = new List<ConstructorInfo>();
        Methods = new Dictionary<string, MethodInfo>();
        Fields = new Dictionary<string, GType>();
    }


}