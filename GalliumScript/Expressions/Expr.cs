namespace GalliumScript.Expressions;

public abstract record Expr
{
    internal abstract T AcceptVisitor<T>(IVisitor<T> visitor);
}