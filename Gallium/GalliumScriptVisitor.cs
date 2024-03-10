using Gallium.AbstractSyntaxTree;

namespace Gallium;

public class GalliumScriptVisitor : GalliumScriptBaseVisitor<ASTNode>
{
    public override ASTNode VisitVarDecl(GalliumScriptParser.VarDeclContext context)
    {
        var identifier = context.IDENTIFIER().GetText();
        var expression = context.expression();


        ASTNode? expressionNode = null;
        if (expression != null)
        {
            expressionNode = Visit(expression);
        }

        return new VarDeclNode(identifier, expressionNode);
    }



    public override ASTNode VisitTrueConstantExpression(GalliumScriptParser.TrueConstantExpressionContext context)
    {
        return new BooleanConstantExpressionNode(true);
    }

    public override ASTNode VisitFalseConstantExpression(GalliumScriptParser.FalseConstantExpressionContext context)
    {
        return new BooleanConstantExpressionNode(false);
    }


    public override ASTNode VisitNumericConstantExpression(GalliumScriptParser.NumericConstantExpressionContext context)
    {
        var expression = context.NUMBER().GetText();
        return new NumberConstantExpressionNode(double.Parse(expression));
    }

    public override ASTNode VisitStringConstantExpression(GalliumScriptParser.StringConstantExpressionContext context)
    {
        var expression = context.STRING().GetText();
        return new StringConstantExpression(expression[1..^1]);
    }

    public override ASTNode VisitIdentifierExpression(GalliumScriptParser.IdentifierExpressionContext context)
    {
        var identifer = context.IDENTIFIER().GetText();
        return new IdentifierExpressionNode(identifer);
    }

    public override ASTNode VisitProgram(GalliumScriptParser.ProgramContext context)
    {
        var nodes = context.declaration()
            .Select(Visit)
            .ToList();

        return new ProgramNode(nodes);
    }

    public override ASTNode VisitTerm(GalliumScriptParser.TermContext context)
    {

        var termNodes = new List<ASTNode>();

        var ops = new List<string>();
        foreach (var op in context.term_op())
        {
            ops.Add(op.GetText());
        }

        var operands = new List<ASTNode>();
        foreach (var factor in context.factor())
        {
            var factorNode = Visit(factor);
            operands.Add(factorNode);
        }

        var opIndex = 0;
        for (int i = 0; i < operands.Count - 1; i++)
        {
            termNodes.Add(new TermNode(operands[i], operands[i+1], ops[opIndex++]));
        }

        return base.VisitTerm(context);
    }

    public override ASTNode VisitBlock(GalliumScriptParser.BlockContext context)
    {
        var nodes = context.declaration()
            .Select(Visit)
            .ToList();

        return new BlockNode(nodes);
    }
}