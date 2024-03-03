using GalliumScript.Expressions;

namespace GalliumScript.Statements;

public class PrintStatement : Stmt
{
    public IList<Expr> Expressions { get;  }

    public PrintStatement(IList<Expr> expressions)
    {
        Expressions = expressions;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}