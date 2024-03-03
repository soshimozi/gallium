namespace GalliumScript.Statements;

public class BlockStatement : Stmt
{
    public IList<Stmt> Statements { get; }

    public BlockStatement(IList<Stmt> statements)
    {
        Statements = statements;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}