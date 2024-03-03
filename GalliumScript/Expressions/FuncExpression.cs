using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class FuncExpression(Expr Callee, Token RightParen, IList<Expr> Args) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}