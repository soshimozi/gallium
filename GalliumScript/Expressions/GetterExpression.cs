using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class GetterExpression(Expr GetFrom, Token Name) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}