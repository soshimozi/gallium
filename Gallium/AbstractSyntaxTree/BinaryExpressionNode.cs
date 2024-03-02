namespace Gallium.AbstractSyntaxTree;

public class BinaryExpressionNode : ASTNode
{
    public ASTNode LeftHandSide { get; init; }
    public ASTNode RightHandSide { get; init;}

    public string Operation { get; set; }

    public BinaryExpressionNode(ASTNode leftHandSide, ASTNode rightHandSide, string operation)
    {
        LeftHandSide = leftHandSide;
        RightHandSide = rightHandSide;
        Operation = operation;
    }
}