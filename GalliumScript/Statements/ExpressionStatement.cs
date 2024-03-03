using GalliumScript.Expressions;

namespace GalliumScript.Statements;

public class ExpressionStatement : Stmt
{
    public Expr Expression { get;  }

    public ExpressionStatement(Expr expression)
    {
        Expression = expression;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}