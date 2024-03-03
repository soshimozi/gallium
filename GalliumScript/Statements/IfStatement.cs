using GalliumScript.Expressions;

namespace GalliumScript.Statements;

public class IfStatement : Stmt
{
    public Expr Condition { get;  }
    public Stmt ThenBranch { get;  }
    public Stmt? ElseBranch { get; }

    public IfStatement(Expr condition, Stmt thenBranch, Stmt? elseBranch)
    {
        Condition = condition;
        ThenBranch = thenBranch;
        ElseBranch = elseBranch;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}