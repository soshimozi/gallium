using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class FunctionDeclarationNode : ASTNode
{
    public string Name { get; init; }
    public TypeInfo? Type { get; init; }

    public ASTNode? Body { get; init; }
    public FunctionDeclarationNode(string name, TypeInfo type, ASTNode? body = null)
    {
        Name = name;
        Type = type;
        Body = body;
    }
}