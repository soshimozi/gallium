namespace Gallium.AbstractSyntaxTree;

public class BlockNode : ASTNode
{
    public List<ASTNode> Declarations { get; set; }

    public BlockNode(List<ASTNode> declarations)
    {
        Declarations = declarations;
    }
}