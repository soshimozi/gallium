namespace GalliumScript.Expressions;

public record class ListExpression(IList<Expr> Elements) : Expr
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}