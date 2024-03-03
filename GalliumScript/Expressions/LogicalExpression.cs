using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class LogicalExpression(Expr Left, Token Op, Expr Right) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}