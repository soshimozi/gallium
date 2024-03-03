using GalliumScript.Expressions;
using GalliumScript.Tokens;

namespace GalliumScript.Statements;

public class ClassStatement : Stmt
{
    public Token Name { get; }
    public VariableExpression? Superclass { get;  }
    public IList<FunctionStatement> Methods { get; }

    public ClassStatement(Token name, VariableExpression? superClass, IList<FunctionStatement> methods)
    {
        Name = name;
        Superclass = superClass;
        Methods = methods;
    }

    internal override T AcceptVisitor<T>(IVisitor<T> visitor) => visitor.Visit(this);

}