namespace Gallium.AbstractSyntaxTree;

public class IntegerConstantNode : ASTNode
{
    public int Value { get; init; }

    public IntegerConstantNode(int value)
    {
        Value = value;
    }
}