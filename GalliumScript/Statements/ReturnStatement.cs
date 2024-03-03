using GalliumScript.Expressions;
using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class ReturnStatement : Stmt
{
    public Token Keyword { get; }
    public Expr? Expression { get; }

    public ReturnStatement(Token keyword, Expr? expression)
    {
        Keyword = keyword;
        Expression = expression;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}