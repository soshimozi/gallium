namespace GalliumScript.Expressions;

public record class TernaryExpression(Expr Conditional, Expr IfTrue, Expr IfFalse) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}