using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class AssignmentNode : ASTNode
{
    public SymbolInfo Identifier { get; set; }
    public ASTNode AssignmentExpression { get; set; }

    public AssignmentNode(SymbolInfo identifier, ASTNode assignmentExpression)
    {
        Identifier = identifier;
        AssignmentExpression = assignmentExpression;
    }
}