using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class BreakStatement : Stmt
{
    public  Token Keyword { get; }

    public BreakStatement(Token keyword)
    {
        Keyword = keyword;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}