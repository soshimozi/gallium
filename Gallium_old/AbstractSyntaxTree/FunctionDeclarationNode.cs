using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class FunctionDeclarationNode : ASTNode
{
    public string Name { get; init; }
    public TypeInfo Parent { get; init; }
    public TypeInfo ReturnType { get; set; }

    public ASTNode? Body { get; init; }
    public FunctionDeclarationNode(string name, TypeInfo parent, TypeInfo returnType, ASTNode? body = null)
    {
        Name = name;
        ReturnType = returnType;
        Parent = parent;
        Body = body;
    }
}