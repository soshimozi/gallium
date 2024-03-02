using Gallium.Types;

namespace Gallium.AbstractSyntaxTree;

public class IdentifierExpressionNode : ASTNode
{
    public string Identifier { get; init; }
    public SymbolInfo SymbolInfo { get; init; }

    public IdentifierExpressionNode(string identifier, SymbolInfo symbolInfo)
    {
        Identifier = identifier;
        SymbolInfo = symbolInfo;
    }
}