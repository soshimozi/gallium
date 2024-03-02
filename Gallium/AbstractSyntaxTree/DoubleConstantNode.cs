namespace Gallium.AbstractSyntaxTree;

public class DoubleConstantNode : ASTNode
{
    public double Value { get; init; }

    public DoubleConstantNode(double value)
    {
        Value = value;
    }
    
}