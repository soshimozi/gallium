namespace Gallium.AbstractSyntaxTree;

public class NumberConstantExpressionNode : ASTNode
{
    public double Value { get; set; }

    public NumberConstantExpressionNode(double value)
    {
        Value = value;
    }
}