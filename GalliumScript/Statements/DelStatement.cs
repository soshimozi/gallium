using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class DelStatement : Stmt
{
    public IList<Token> Variables { get;  }

    public DelStatement(IList<Token> variables)
    {
        Variables = variables;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}