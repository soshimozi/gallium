namespace GalliumScript.Expressions;

public record class LiteralExpression(object? Value) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}