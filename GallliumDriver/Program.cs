using Antlr4.Runtime;
using Gallium;
using Gallium.Types;

namespace GallliumDriver
{

    internal class Program : ISyntaxErrorHandler
    {
        private const string testScript = "\n" +
                                          "class foobar { \n" +
                                          " var foo: int;\n" +
                                          " function baz(a: int, b: int) : none {\n"+
                                          "     print (a + 23) + b;\n" +
                                          " }\n"+
                                          "}\n" +
                                          "\n"+
                                          "function foo(a: int, b: int) : int { \n" +
                                          "     print a; \n" +
                                          "     return 23;\n" +
                                          "}\n\n" +
                                          "var f:foobar = new();\n" +    
                                          "f.baz(23, 43); \n" +
                                          "foo(23, 43);";


        private List<string> _errors = new List<string>();

        public void Run(string[] args)
        {
            var stream = new AntlrInputStream(testScript);
            //var lexer = new LogicSimulator.Parser.

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