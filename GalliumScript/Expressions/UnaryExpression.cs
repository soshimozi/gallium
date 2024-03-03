using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class UnaryExpression(Token Op, Expr Expression) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}