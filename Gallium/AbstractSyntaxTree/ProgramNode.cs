namespace Gallium.AbstractSyntaxTree;

public class ProgramNode : ASTNode
{
    public List<ASTNode> Declarations { get; set; }

    public ProgramNode(List<ASTNode> declarations)
    {
        Declarations = declarations;
    }
}