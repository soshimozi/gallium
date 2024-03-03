using GalliumScript.Expressions;

namespace GalliumScript.Statements;

public class WhileStatement : Stmt
{
    public Expr Condition { get; }
    public Stmt Body { get; }

    public WhileStatement(Expr condition, Stmt body)
    {
        Condition = condition;
        Body = body;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}