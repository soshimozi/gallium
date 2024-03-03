using Gallium.AbstractSyntaxTree;

namespace Gallium;

public class GalliumScriptVisitor : GalliumScriptBaseVisitor<ASTNode>
{
    public override ASTNode VisitProgram(GalliumScriptParser.ProgramContext context)
    {
        var nodes = context.declaration()
            .Select(Visit)
            .ToList();

        return new ProgramNode(nodes);
    }
}