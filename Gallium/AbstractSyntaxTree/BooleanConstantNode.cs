namespace Gallium.AbstractSyntaxTree;

public class BooleanConstantNode : ASTNode
{
    public bool Value { get; init; }

    public BooleanConstantNode(bool value)
    {
        Value = value;
    }

}