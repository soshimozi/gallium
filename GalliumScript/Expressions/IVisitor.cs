namespace GalliumScript.Expressions;

public interface IVisitor<out T>
{
    T Visit(AssignmentExpression expr);
    T Visit(BinaryExpression expr);
    T Visit(FuncExpression expr);
    T Visit(GetterExpression expr);
    T Visit(LiteralExpression expr);
    T Visit(ListExpression expr);
    T Visit(LogicalExpression expr);
    T Visit(SetterExpression expr);
    T Visit(SuperExpression expr);
    T Visit(TernaryExpression expr);
    T Visit(ThisExpression expr);
    T Visit(UnaryExpression expr);
    T Visit(VariableExpression expr);
}