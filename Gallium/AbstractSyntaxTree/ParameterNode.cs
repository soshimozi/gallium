namespace Gallium.AbstractSyntaxTree;

public class ParameterNode : ASTNode
{
    public string Identifier { get; set; }

    public ParameterNode(string identifier)
    {
        Identifier = identifier;
    }

}