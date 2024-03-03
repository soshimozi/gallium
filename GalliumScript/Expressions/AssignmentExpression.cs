using GalliumScript.Tokens;

namespace GalliumScript.Expressions;

public record class AssignmentExpression(Token Name, Expr Value) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}

