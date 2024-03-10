namespace Gallium.AbstractSyntaxTree;

public class VarDeclNode : ASTNode
{
    public string Identifier { get; }
    public ASTNode? Expression { get; }

    public VarDeclNode(string identifier, ASTNode? expression)
    {
        Identifier  = identifier;
        Expression = expression;
    }
}