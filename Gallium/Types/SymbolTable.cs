namespace Gallium.Types;

public class SymbolTable
{
    private readonly Stack<Scope> scopes = new Stack<Scope>();

    public SymbolTable()
    {
        EnterScope();
    }

    public void EnterScope()
    {
        scopes.Push(new Scope());
    }

    public void ExitScope()
    {
        scopes.Pop();
    }

    public void DefineSymbol(string name, TypeInfo type)
    {
        var currentScope = scopes.Peek();
        currentScope.Define(new SymbolInfo(name, type));
    }

    public SymbolInfo? ResolveSymbol(string name)
    {
        return scopes
            .Select(scope => scope.Resolve(name))
            .FirstOrDefault(symbol => symbol != null);
    }

}