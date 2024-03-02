using Antlr4.Runtime;

namespace GallliumDriver;

public interface ISyntaxErrorHandler
{
    void OnSyntaxError(int line, int col, string msg, RecognitionException e);
}