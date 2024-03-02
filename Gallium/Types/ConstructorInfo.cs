namespace Gallium.Types;

public class ConstructorInfo
{
    public List<ParameterInfo> Parameters { get; set; }
    public ConstructorInfo(List<ParameterInfo> parameters)
    {
        Parameters = parameters;
    }
}