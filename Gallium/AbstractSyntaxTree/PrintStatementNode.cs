namespace Gallium.AbstractSyntaxTree;

public class PrintStatementNode : ASTNode
{
    public ASTNode Expression { get; init; }

    public PrintStatementNode(ASTNode expression)
    {
        Expression = expression;
    }
}