using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class FunctionCallNode : ASTNode
{
    public string Name { get; set; }
    public List<ASTNode>? Parameters { get; set; }
    public MethodInfo MethodInfo { get; set; }

    public FunctionCallNode(string name, MethodInfo methodInfo, List<ASTNode>? parameters)
    {
        Name = name;
        Parameters = parameters;
        MethodInfo = methodInfo;
    }
}