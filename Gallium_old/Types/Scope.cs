namespace Gallium.Types;

public class Scope
{
    public Dictionary<string, SymbolInfo> Symbols { get; } = new Dictionary<string, SymbolInfo>();

    public void Define(SymbolInfo symbol)
    {
        Symbols[symbol.Name] = symbol;
    }

    public SymbolInfo? Resolve(string name)
    {
        Symbols.TryGetValue(name, out var symbolInfo);
        return symbolInfo;
    }
}