using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class MethodInvocationNode : ASTNode
{
    public MethodInfo MethodInfo { get; set; }

    public MethodInvocationNode(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
    }
}