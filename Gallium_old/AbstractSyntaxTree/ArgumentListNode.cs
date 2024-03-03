namespace Gallium.AbstractSyntaxTree;

public class ArgumentListNode : ASTNode
{
    public List<ASTNode> ExpressionNodes { get; } = new List<ASTNode>();

    
}