using Antlr4.Runtime;
using Gallium;
using Gallium.Types;

namespace GallliumDriver
{

    internal class Program : ISyntaxErrorHandler
    {
        private List<string> _errors = new List<string>();

        public void Run(string[] args)
        {
            var stream = new AntlrInputStream(File.Open("test.gs", FileMode.Open));

            var lexer = new GalliumScriptLexer(stream);
            var tokens = new CommonTokenStream(lexer);

            var parser = new GalliumScriptParser(tokens);

            var lexerErrorListener = new SyntaxErrorHandlerAdapter<int>(this);
            var parserErrorListener = new SyntaxErrorHandlerAdapter<IToken>(this);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(lexerErrorListener);

            parser.RemoveErrorListeners();
            parser.AddErrorListener(parserErrorListener);

            var programTree = parser.program();

            if (_errors.Count > 0)
            {
                return;
            }

            var registry = new TypeRegistry();
            var symbolTable = new SymbolTable();

            var visitor = new GalliumScriptVisitor(registry, symbolTable);
            var program = visitor.Visit(programTree);
        }

        static void Main(string[] args)
        {
            var program = new Program();

            program.Run(args);
        }

        public void OnSyntaxError(int line, int col, string msg, RecognitionException e)
        {
            _errors.Add(msg);

            //Console.WriteLine($"Error at {line}:{col} - {msg}");
        }
    }
}