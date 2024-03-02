using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class MethodInvocationNode : ASTNode
{
    public string MethodName { get; set; }
    public TypeInfo ReturnType { get; set; }

    public MethodInvocationNode(string methodName, TypeInfo returnType)
    {
        MethodName = methodName;
        ReturnType = returnType;
    }
}