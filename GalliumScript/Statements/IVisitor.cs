namespace GalliumScript.Statements;

public interface IVisitor<out T>
{
    T Visit(AssertStatement stmt);
    T Visit(BlockStatement stmt);
    T Visit(BreakStatement stmt);
    T Visit(ContinueStatement stmt);
    T Visit(ClassStatement stmt);
    T Visit(DelStatement stmt);
    T Visit(ExpressionStatement stmt);
    T Visit(FunctionStatement stmt);
    T Visit(IfStatement stmt);
    T Visit(PrintStatement stmt);
    T Visit(ReturnStatement stmt);
    T Visit(VarStatement stmt);
    T Visit(WhileStatement stmt);
}