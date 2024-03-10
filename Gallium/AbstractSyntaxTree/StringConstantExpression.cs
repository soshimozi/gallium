namespace Gallium.AbstractSyntaxTree;

public class StringConstantExpression : ASTNode
{
    public string Value { get; }

    public StringConstantExpression(string value)
    {
        Value = value;
    }
}