using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class VariableExpression(Token Name) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}