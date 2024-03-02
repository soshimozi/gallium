using Antlr4.Runtime;

namespace GallliumDriver;

public class SyntaxErrorHandlerAdapter<T> : ConsoleErrorListener<T>
{
    private readonly ISyntaxErrorHandler _listener;
    public SyntaxErrorHandlerAdapter(ISyntaxErrorHandler listener)
    {
        _listener = listener;
    }

    public override void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)
    {
        _listener.OnSyntaxError(line, charPositionInLine, msg, e);

        base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
    }
}