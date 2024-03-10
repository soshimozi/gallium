namespace Gallium.AbstractSyntaxTree;

public class TermNode : ASTNode
{
    public ASTNode Left { get; set; }
    public ASTNode Right { get; set; }
    public string Operator { get; }

    public TermNode(ASTNode left, ASTNode right, string @operator)
    {
        Left = left;
        Right = right;
        Operator = @operator;
    }
}