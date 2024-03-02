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

    public override ASTNode VisitFunctionDeclaration(GalliumScriptParser.FunctionDeclarationContext context)
    {
        var name = context.IDENTIFIER().GetText();

        if (_symbolTable.ResolveSymbol(name) != null)
        {
            return OnError(context, $"Duplicate function found {name}");
        }

        var functionReturnType = context.type().GetText();
        var functionReturnTypeInfo = _typeRegistry.GetType(functionReturnType);

        if (functionReturnTypeInfo == null)
        {
            return OnError(context, $"Could not find type {functionReturnType}");
        }

        var typeInfo = _typeRegistry.GetType(_typeNameStack.Peek());
        if (typeInfo == null)
        {
            return OnError(context, $"Invalid type {_typeNameStack.Peek()}");
        }

        _symbolTable.DefineSymbol(name, typeInfo);

        // everything defined from this point should be in a new scope for the function
        _symbolTable.EnterScope();

        var parameters = new List<SymbolInfo>();

        if (context.functionParametersDecl()?.functionParameterDecl() != null)
        {
            foreach (var parameterDecl in context.functionParametersDecl()
                         .functionParameterDecl())
            {
                var parameterType = GetTypeFromString(parameterDecl.type().GetText());

                if (parameterType != null)
                {
                    parameters.Add(new SymbolInfo(parameterDecl.IDENTIFIER().GetText(), parameterType));
                }
            }
        }

        foreach (var parameter in parameters)
        {
            _symbolTable.DefineSymbol(parameter.Name, parameter.Type);
        }

        var block = Visit(context.block());

        _symbolTable.ExitScope();

        typeInfo = _typeRegistry.GetType(_typeNameStack.Peek());
        if (typeInfo == null)
        {
            // handle error
            return OnError(context, "Could not find base type.");
        }

        if (typeInfo.Methods.ContainsKey(name))
        {
            return OnError(context, $"Duplicate method found {name}");
        }


        typeInfo.Methods.Add(name, new MethodInfo(typeInfo.Name, name, functionReturnTypeInfo, parameters, false));
        return new FunctionDeclarationNode(name, typeInfo, functionReturnTypeInfo, block);
    }

    public override ASTNode VisitIdentifierExpression(GalliumScriptParser.IdentifierExpressionContext context)
    {
        var symbol = _symbolTable.ResolveSymbol(context.IDENTIFIER().GetText());
        return symbol == null ? 
            OnError(context, $"Symbol not found: {symbol}") 
            : new IdentifierExpressionNode(context.IDENTIFIER().GetText(), symbol);
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
        _typeNameStack.Push(className);

        _symbolTable.EnterScope();

        var constructorList = context.constructorDeclaration()
            .Select(Visit)
            .ToList();

        var bodyDeclarations = context.classBodyDeclaration()
            .Select(Visit)
            .ToList();

        _symbolTable.ExitScope();
        _typeNameStack.Pop();

        return new ClassDeclarationNode(constructorList, bodyDeclarations);
    }

    public override ASTNode VisitNewObjectExpression(GalliumScriptParser.NewObjectExpressionContext context)
    {
        if (context.argumentList() != null)
        {
            var arguments = Visit(context.argumentList());
            return new NewObjectNode(arguments);
        }

        return new NewObjectNode();
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

        var symbol = _symbolTable.ResolveSymbol(functionName);
        if (symbol == null)
        {
            throw new Exception($"Symbol not found '{functionName}'");
        }

        var containingType = _typeRegistry.GetType(symbol.Type.Name);
        if (containingType == null)
        {
            return OnError(context, $"Could not find {symbol.Type.Name} in registry.");
        }

        var methodInfo = containingType.Methods
            .Where(p => p.Key == functionName)
            .Select(p => p.Value)
            .FirstOrDefault();

        if (methodInfo == null)
        {
            return OnError(context, $"Method {functionName} not found on type {symbol.Type.Name}");
        }

        if (context.argumentList() == null) return new FunctionCallNode(functionName, methodInfo, null);

        var arguments = Visit(context.argumentList()) as ArgumentListNode;
        return new FunctionCallNode(functionName, methodInfo, arguments?.ExpressionNodes);

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
        return new StringConstantNode(text[1..^1]);
    }

    public override ASTNode VisitLiteralConstantExpression(GalliumScriptParser.LiteralConstantExpressionContext context)
    {
        return Visit(context.literal());
    }

    public override ASTNode VisitBinaryOperationExpression(GalliumScriptParser.BinaryOperationExpressionContext context)
    {
        var left = Visit(context.expression()[0]);
        var right = Visit(context.expression()[1]);

        return new BinaryExpressionNode(left, right, context.BINARY_OPERATOR().GetText());
    }

    public override ASTNode VisitMethodInvocationExpression(GalliumScriptParser.MethodInvocationExpressionContext context)
    {
        var expression = Visit(context.expression());
        var methodName = context.IDENTIFIER().GetText();

        switch (expression)
        {
            case IdentifierExpressionNode identifier:
            {
                var typeInfo = identifier.SymbolInfo.Type;

                // now find the method which matches this name.  We aren't allowing overloads, so no need to match signatures.
                return !typeInfo.Methods.TryGetValue(methodName, out var methodInfo) ? 
                    OnError(context, $"Could not find method {methodName} on type {typeInfo.Name}") 
                    : new MethodInvocationNode(methodInfo);
            }
            case MethodInvocationNode mi:
            {
                return !mi.MethodInfo.Type.Methods.TryGetValue(methodName, out var methodInfo) ? 
                    OnError(context, $"Could not find method {methodName} on type {mi.MethodInfo.Type.Name}") 
                    : new MethodInvocationNode(methodInfo);
            }
            case FunctionCallNode fc:
                // get type from function call node
                return new MethodInvocationNode(fc.MethodInfo);
            default:
                return OnError(context, "Invalid expression");
        }
    }

    public override ASTNode VisitBinaryConstantExpression(GalliumScriptParser.BinaryConstantExpressionContext context)
    {
        return base.VisitBinaryConstantExpression(context);
    }

    public override ASTNode VisitBitwiseConstantExpression(GalliumScriptParser.BitwiseConstantExpressionContext context)
    {
        return base.VisitBitwiseConstantExpression(context);
    }

    public override ASTNode VisitBitwiseExpression(GalliumScriptParser.BitwiseExpressionContext context)
    {
        return base.VisitBitwiseExpression(context);
    }

    public override ASTNode VisitBooleanLiteral(GalliumScriptParser.BooleanLiteralContext context)
    {
        return base.VisitBooleanLiteral(context);
    }

    public override ASTNode VisitConditionalOperationExpression(GalliumScriptParser.ConditionalOperationExpressionContext context)
    {
        return base.VisitConditionalOperationExpression(context);
    }


    public override ASTNode VisitForStmt(GalliumScriptParser.ForStmtContext context)
    {
        return base.VisitForStmt(context);
    }


    public override ASTNode VisitIfStmt(GalliumScriptParser.IfStmtContext context)
    {
        return base.VisitIfStmt(context);
    }

    public override ASTNode VisitWhileStmt(GalliumScriptParser.WhileStmtContext context)
    {
        return base.VisitWhileStmt(context);
    }

    public override ASTNode VisitSwitchStmt(GalliumScriptParser.SwitchStmtContext context)
    {
        return base.VisitSwitchStmt(context);
    }

    public override ASTNode VisitBreakStmt(GalliumScriptParser.BreakStmtContext context)
    {
        return new BreakStatementNode();
    }


    public override ASTNode VisitAssignmentExpression(GalliumScriptParser.AssignmentExpressionContext context)
    {
        var identifier = context.IDENTIFIER().GetText();

        var symbolInfo = _symbolTable.ResolveSymbol(identifier);
        if (symbolInfo == null)
        {
            return OnError(context, $"Invalid symbol {identifier}");
        }

        return new AssignmentNode(symbolInfo, Visit(context.expression()));
    }

    private static ASTNode OnError(ParserRuleContext ctx, string message)
    {
        return new ErrorNode(ctx.Start.Line, ctx.Start.Column, message);
    }

    private TypeInfo? GetTypeFromString(string typeName)
    {
        return _typeRegistry.GetType(typeName);
    }

}