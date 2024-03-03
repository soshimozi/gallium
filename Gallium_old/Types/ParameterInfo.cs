namespace Gallium.Types;

public class ParameterInfo
{
    public string Name { get; set; }
    public GType Type { get; set; }

    public ParameterInfo(string name, GType type)
    {
        Name = name;
        Type = type;
    }
}