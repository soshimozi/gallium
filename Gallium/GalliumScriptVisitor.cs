using System.Diagnostics.CodeAnalysis;
using System.IO.Enumeration;
using System.Net.Mime;
using Antlr4.Runtime;
using Gallium.AbstractSyntaxTree;
using Gallium.Types;

namespace Gallium;

public class GalliumScriptVisitor : GalliumScriptBaseVisitor<ASTNode>
{
    private readonly SymbolTable _symbolTable;
    private readonly TypeRegistry _typeRegistry;

    private Stack<string> typeNameStack = new Stack<string>();

    public GalliumScriptVisitor(TypeRegistry typeRegistry, SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
        _typeRegistry = typeRegistry;

        _typeRegistry.AddType("global", new TypeInfo("global"));
        _typeRegistry.AddType("int", new TypeInfo("int"));
        _typeRegistry.AddType("double", new TypeInfo("double"));
        _typeRegistry.AddType("string", new TypeInfo("string"));

        typeNameStack.Push("global");
    }

    public override ASTNode VisitFunctionDeclaration(GalliumScriptParser.FunctionDeclarationContext context)
    {
        var name = context.IDENTIFIER().GetText();

        if (_symbolTable.ResolveSymbol(name) != null)
        {
            return base.VisitFunctionDeclaration(context);
        }

        var type = context.type().GetText();

        _symbolTable.DefineSymbol(name, new TypeInfo(name));

        // everything defined from this point should be in a new scope for the function
        _symbolTable.EnterScope();

        var parameters = new List<SymbolInfo>();
        foreach (var parameterDecl in context.functionParametersDecl()
                     .functionParameterDecl())
        {
            var parameterType = GetTypeFromString(parameterDecl.type().GetText());

            if (parameterType != null)
            {
                parameters.Add(new SymbolInfo(parameterDecl.IDENTIFIER().GetText(), parameterType));
            }
        }

        foreach (var parameter in parameters)
        {
            _symbolTable.DefineSymbol(parameter.Name, parameter.Type);
        }

        var block = Visit(context.block());

        _symbolTable.ExitScope();

        // put block somewhere
        var typeInfo = _typeRegistry.GetType(typeNameStack.Peek());
        if (typeInfo == null)
        {
            // handle error
            return OnError(context, "Could not find basetype.");
        }

        typeInfo.Methods.Add(name, new MethodInfo(name, GetTypeFromString(type), parameters, false));
        return new FunctionDeclarationNode(name, typeInfo, block);
    }

    public override ASTNode VisitIdentifierExpression(GalliumScriptParser.IdentifierExpressionContext context)
    {
        var symbol = _symbolTable.ResolveSymbol(context.IDENTIFIER().GetText());
        if (symbol == null)
        {
            return OnError(context, $"Symbol not found: {symbol}");
        }

        return new IdentifierExpressionNode(context.IDENTIFIER().GetText(), symbol);
    }

    public override ASTNode VisitParenthesizedExpression(GalliumScriptParser.ParenthesizedExpressionContext context)
    {
        var expressionNode = Visit(context.expression());
        return expressionNode;
    }

    public override ASTNode VisitClassDeclaration(GalliumScriptParser.ClassDeclarationContext context)
    {
        var className = context.IDENTIFIER().GetText();

        // does this typename already exist?
        if (_symbolTable.ResolveSymbol(className) != null)
        {
            // duplicate classname
            return OnError(context, $"Duplicate type name: {className}");
        }

        var typeInfo = new TypeInfo(className);

        _symbolTable.DefineSymbol(className, typeInfo);

        _typeRegistry.AddType(className, typeInfo);

        // now we have to build the type
        typeNameStack.Push(className);

        _symbolTable.EnterScope();

        var constructorList = context.constructorDeclaration()
            .Select(Visit)
            .ToList();

        var bodyDeclarations = context.classBodyDeclaration()
            .Select(Visit)
            .ToList();
        

        _symbolTable.ExitScope();
        typeNameStack.Pop();

        return new ClassDeclarationNode(constructorList, bodyDeclarations);
    }

    public override ASTNode VisitNewObjectExpression(GalliumScriptParser.NewObjectExpressionContext context)
    {
        var arguments = Visit(context.argumentList());
        return new NewObjectNode(arguments);
    }

    public override ASTNode VisitBlock(GalliumScriptParser.BlockContext context)
    {
        var declarations = context.declaration()
            .Select(Visit)
            .ToList();

        return new BlockNode(declarations);
    }

    public override ASTNode VisitVarDeclaration(GalliumScriptParser.VarDeclarationContext context)
    {
        var name = context.IDENTIFIER().GetText();
        
        if(_symbolTable.ResolveSymbol(name) != null) { return OnError(context, $"Duplicate variable definition: {name}"); }

        var type = GetTypeFromString(context.type().GetText());

        if (type == null) return OnError(context, $"Could not find type for {context.type().GetText()}");
        _symbolTable.DefineSymbol(name, type);

        return new VarDeclarationNode(name, context.type().GetText());
    }

    public override ASTNode VisitPrintStmt(GalliumScriptParser.PrintStmtContext context)
    {
        var expression = Visit(context.expression());
        return new PrintStatementNode(expression);
    }

    public override ASTNode VisitReturnStmt(GalliumScriptParser.ReturnStmtContext context)
    {
        var expression = Visit(context.expression());
        return new ReturnStatementNode(expression);
    }

    public override ASTNode VisitProgram(GalliumScriptParser.ProgramContext context)
    {
        var list = context.declaration()
            .Select(Visit)
            .ToList();

        return new ProgramNode(list);
    }

    public override ASTNode VisitExprStmt(GalliumScriptParser.ExprStmtContext context)
    {
        return Visit(context.expression());
    }

    public override ASTNode VisitArgumentList(GalliumScriptParser.ArgumentListContext context)
    {
        var argumentListNode = new ArgumentListNode();
        foreach (var expr in context.expression())
        {
            argumentListNode.ExpressionNodes.Add(Visit(expr));
        }

        return argumentListNode;
    }

    public override ASTNode VisitFunctionCall(GalliumScriptParser.FunctionCallContext context)
    {
        var functionName = context.IDENTIFIER().GetText();

        if (_symbolTable.ResolveSymbol(functionName) == null)
        {
            throw new Exception($"Symbol not found '{functionName}'");
        }

       
        var arguments = Visit(context.argumentList()) as ArgumentListNode;

        return new FunctionCallNode(functionName, arguments?.ExpressionNodes);
    }

    public override ASTNode VisitIntegerLiteral(GalliumScriptParser.IntegerLiteralContext context)
    {
        var value = int.Parse(context.INT_LITERAL().GetText());
        return new IntegerConstantNode(value);
    }

    public override ASTNode VisitDoubleLiteral(GalliumScriptParser.DoubleLiteralContext context)
    {
        return new DoubleConstantNode(double.Parse(context.DOUBLE_LITERAL().GetText()));
    }

    public override ASTNode VisitStringLiteral(GalliumScriptParser.StringLiteralContext context)
    {
        var text = context.STRING_LITERAL().GetText();
        return new StringConstantNode(text.Substring(1, text.Length - 2));
    }

    public override ASTNode VisitLiteralConstantExpression(GalliumScriptParser.LiteralConstantExpressionContext context)
    {
        return Visit(context.literal());
    }

    public override ASTNode VisitBinaryOperationExpression(GalliumScriptParser.BinaryOperationExpressionContext context)
    {
        var left = Visit(context.expression()[0]);
        var right = Visit(context.expression()[1]);

        return new BinaryExpressionNode(left, right);
    }

    public override ASTNode VisitMethodInvocationExpression(GalliumScriptParser.MethodInvocationExpressionContext context)
    {
        var expression = Visit(context.expression());

        if (expression is IdentifierExpressionNode identifier)
        {
            var symbol = _symbolTable.ResolveSymbol(identifier.SymbolInfo.Name);
            if (symbol == null)
            {
                return OnError(context, $"Symbol not found {identifier.SymbolInfo.Name}");
            }

            var typeInfo = _typeRegistry.GetType(symbol.Type.Name);
            if (typeInfo == null)
            {
                return OnError(context, $"Could not find type for {symbol.Type.Name}");
            }

            // now find the method which matches this name.  We aren't allowing overloads, so no need to match signatures.
            if(!typeInfo.Methods.TryGetValue(symbol.Type.Name, out var methodInfo))
            {
                return OnError(context, $"Could not find method {symbol.Type.Name} on type {typeInfo.Name}");
            }

            return new MethodInvocationNode(identifier.Identifier, methodInfo.Type);

        }
        
        if(expression is MethodInvocationNode mi)
        {
            // get return type
            // then look up in registry

            // find class in registry

            // get type from method invocation
            return new MethodInvocationNode("", new TypeInfo(""));
        }

        if (expression is FunctionCallNode fc)
        {
            // get type from function call node
            return new MethodInvocationNode("", new TypeInfo(""));
        }

        return OnError(context, $"Invalid expression");
    }

    private ASTNode OnError(ParserRuleContext ctx, string message)
    {
        return new ErrorNode(ctx.Start.Line, ctx.Start.Column, message);
    }

    private TypeInfo? GetTypeFromString(string typeName)
    {
        return _typeRegistry.GetType(typeName);
    }

}