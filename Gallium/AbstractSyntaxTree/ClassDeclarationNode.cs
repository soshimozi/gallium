using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class ClassDeclarationNode : ASTNode
{
    public List<ASTNode> ConstructorNodes { get; set; }
    public List<ASTNode> BodyDeclarations { get; set; }
    public TypeInfo? Parent { get; set; }
    public ClassDeclarationNode(List<ASTNode> constructorNodes, List<ASTNode> bodyDeclarations, TypeInfo? parent)
    {
        ConstructorNodes = constructorNodes;
        BodyDeclarations = bodyDeclarations;
        Parent = parent;
    }
}