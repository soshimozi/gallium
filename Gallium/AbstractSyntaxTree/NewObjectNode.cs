using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class NewObjectNode : ASTNode
{
    public ASTNode? ArgumentListNode { get; }

    public NewObjectNode(ASTNode? argumentListNode = null)
    {
        ArgumentListNode = argumentListNode;
    }
}