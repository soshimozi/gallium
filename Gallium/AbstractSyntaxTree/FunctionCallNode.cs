namespace Gallium.AbstractSyntaxTree;

public class FunctionCallNode : ASTNode
{
    public string Name { get; set; }
    public List<ASTNode>? Parameters { get; set; }

    public FunctionCallNode(string name, List<ASTNode>? parameters)
    {
        Name = name;
        Parameters = parameters;
    }
}