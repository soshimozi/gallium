using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class ThisExpression(Token Keyword) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}