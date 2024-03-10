namespace Gallium.AbstractSyntaxTree;

public class BooleanConstantExpressionNode : ASTNode
{
    public bool Value { get; set; }

    public BooleanConstantExpressionNode(bool value)
    {
        Value = value;
    }
}