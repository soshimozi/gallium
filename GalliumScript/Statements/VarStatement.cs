using GalliumScript.Expressions;
using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class VarStatement : Stmt
{
    public Token Name { get; }
    public Expr? Initializer { get; }

    public VarStatement(Token name, Expr? initializer)
    {
        Name = name;
        Initializer = initializer;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);
}