namespace GalliumScript.Statements;

public class ContinueStatement : Stmt
{
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}