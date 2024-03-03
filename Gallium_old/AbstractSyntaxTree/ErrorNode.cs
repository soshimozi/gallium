namespace Gallium.AbstractSyntaxTree;

public class ErrorNode : ASTNode
{
    public int LineNumber { get; }
    public int ColumnNumber { get;}
    public string Message { get; }

    public ErrorNode(int lineNumber, int columnNumber, string message)
    {
        LineNumber = lineNumber;
        ColumnNumber = columnNumber;
        Message = message;
    }
}