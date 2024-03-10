namespace Gallium.AbstractSyntaxTree;

public class IdentifierExpressionNode : ASTNode
{
    public string Identifier { get; set; }
    public IdentifierExpressionNode(string identifier)
    {
        Identifier = identifier;
    }
}