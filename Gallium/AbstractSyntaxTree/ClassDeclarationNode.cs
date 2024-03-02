namespace Gallium.AbstractSyntaxTree;

public class ClassDeclarationNode : ASTNode
{
    public List<ASTNode> ConstructorNodes { get; set; }
    public List<ASTNode> BodyDeclarations { get; set; }

    public ClassDeclarationNode(List<ASTNode> constructorNodes, List<ASTNode> bodyDeclarations)
    {
        ConstructorNodes = constructorNodes;
        BodyDeclarations = bodyDeclarations;
    }
}