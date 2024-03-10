namespace Gallium.AbstractSyntaxTree;

public class BlockNode : ASTNode
{
    public List<ASTNode> Declarations { get; }

    public BlockNode(List<ASTNode> declarations)
    {
        Declarations = declarations;
    }
}