namespace Gallium.AbstractSyntaxTree;

public class VarDeclarationNode : ASTNode
{
    public string Name { get; }
    public string Type { get; }
    public ASTNode? Expression { get;}

    public VarDeclarationNode(string name, string type, ASTNode? expression = null)
    {
        Name = name;
        Type = type;
        Expression = expression;
    }
}