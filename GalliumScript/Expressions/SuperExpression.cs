using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class SuperExpression(Token Keyword, Token Obj) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}