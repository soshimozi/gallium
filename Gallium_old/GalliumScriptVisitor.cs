using Antlr4.Runtime;
using Gallium.AbstractSyntaxTree;
using Gallium.Types;

namespace Gallium;

public class GalliumScriptVisitor : GalliumScriptBaseVisitor<ASTNode>
{
    private readonly SymbolTable _symbolTable;
    private readonly TypeRegistry _typeRegistry;
    private readonly Stack<string> _typeNameStack = new();

    public GalliumScriptVisitor(TypeRegistry typeRegistry, SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
        _typeRegistry = typeRegistry;

        _typeRegistry.AddType("global", new TypeInfo("global"));
        _typeRegistry.AddType("int", new TypeInfo("int"));
        _typeRegistry.AddType("double", new TypeInfo("double"));
        _typeRegistry.AddType("string", new TypeInfo("string"));
        _typeRegistry.AddType("none", new TypeInfo("none"));

        _typeNameStack.Push("global");
    }

}