using GalliumScript.Expressions;
using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class AssertStatement : Stmt
{
    public Expr Condition { get;  }
    public Token Keyword { get; }
    public Expr Message { get; }

    public AssertStatement(Expr condition, Token keyword, Expr message)
    {
        Condition = condition;
        Keyword = keyword;
        Message = message;
    }
    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}

