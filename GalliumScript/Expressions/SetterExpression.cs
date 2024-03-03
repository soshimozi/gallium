using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class SetterExpression(Expr Obj, Token Name, Expr Value) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}