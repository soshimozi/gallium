using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class FunctionStatement : Stmt
{

    public Token Name { get; }
    public IList<Token> Parameters { get; }
    public BlockStatement Body { get; }

    public FunctionStatement(Token name, IList<Token> parameters, BlockStatement body)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}