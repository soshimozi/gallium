namespace Gallium.AbstractSyntaxTree;

public class StringConstantNode : ASTNode
{
    public string Value { get; init; }

    public StringConstantNode(string value)
    {
        Value = value;
    }
}