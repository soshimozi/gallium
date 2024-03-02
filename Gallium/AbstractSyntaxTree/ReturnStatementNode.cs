namespace Gallium.AbstractSyntaxTree;

public class ReturnStatementNode : ASTNode
{
    public ASTNode Expression { get; init; }

    public ReturnStatementNode(ASTNode expression)
    {
        Expression = expression;
    }

}