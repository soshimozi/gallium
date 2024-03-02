namespace Gallium.AbstractSyntaxTree;

public class BinaryExpressionNode : ASTNode
{
    public ASTNode LeftHandSide { get; init; }
    public ASTNode RightHandSide { get; init;}

    public BinaryExpressionNode(ASTNode leftHandSide, ASTNode rightHandSide)
    {
        LeftHandSide = leftHandSide;
        RightHandSide = rightHandSide;
    }
}