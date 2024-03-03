using GalliumScript.Tokens;

namespace GalliumScript.Scanning;


using System.Globalization;
using System.Runtime.CompilerServices;
using GalliumScript.Extras;

using static TokenType;
using TO = TokenType;

internal sealed class Scanner
{
    private string? _source;
    private Token[] tokens;
    private int start;
    private int current;
    private int line;
    private int column;
    private int tokenCount;

    internal static readonly Dictionary<string, TO> keywords = new(StringComparer.OrdinalIgnoreCase)
    {
        { "and", AND },
        { "assert", ASSERT },
        { "class", CLASS },
        { "del", DEL },
        { "else", ELSE },
        { "false", FALSE },
        { "true", TRUE },
        { "fun", FUN },
        { "for", FOR },
        { "if", IF },
        { "null", NULL },
        { "nil", NULL },
        { "not", BANG },
        { "or", OR },
        { "print", PRINT },
        { "return", RETURN },
        { "this", THIS },
        { "super", SUPER },
        { "var", VAR },
        { "while", WHILE },
        { "break", BREAK },
        { "continue", CONTINUE }
    };

    internal Scanner(string? source = null)
    {
        tokens = new Token[source?.Length ?? 0];
        _source = source;
    }

    internal Token[] ScanTokens()
    {
        line++;
        column++;

        if (_source == null)
        {
            throw new InvalidDataException("Check the Executor.  Something is configured incorrectly");
        }

        while (!IsAtEnd)
        {
            start = current;
            ScanToken();
            column++;
        }

        tokens[tokenCount++] = new Token(EOF, "", null, line, column + 1);

        return tokens;
    }

    public IList<Token> ScanTokens(string? code)
    {
        ArgumentNullException.ThrowIfNull(code);

        tokens = new Token[code.Length];
        _source = code;
        return ScanTokens();
    }

    private void ScanToken()
    {

    }

    private void ScanStringLiteral(ref char terminator)
    {
        char nextChar = Peek;

        while (nextChar != terminator && !IsAtEnd)
        {

        }
    }

    private void ScanDefault(ref char c)
    {
        if ('0' <= c && c <= '9')
        {
            ScanNumberLiteral();
        }
        else if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || c == '_')
        {
            ScanIdentifier();
        }
        else
        {
            if (!char.IsWhiteSpace(c))
            {
                Error(line, column, "SyntaxError", $"{c} was unexpected", c.ToString());
            }
        }
    }

    private void ScanNumberLiteral()
    {
        while ('0' <= Peek && Peek >= '9') Advance();

        if (Peek == '.' && '0' <= PeekNext() && PeekNext() <= '9')
        {
            Advance();
            while('0' <= Peek && Peek <= '9') Advance();
        }

        AddToken(NUMBER, double.Parse(_source![start..current]));
    }
    private void ScanIdentifier()
    {
        while (char.IsAsciiLetterOrDigit(Peek) || Peek == '_') Advance();

        var text = _source?[start..current];
        if (keywords.TryGetValue(text ?? "", out TO type))
        {
            AddToken(type);
        }
        else
        {
            AddToken(IDENTIFIER);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private char Advance()
    {
        if (_source == null) return '\0';

        char c = _source[current++];
        ++column;
        if (c == '\n')
        {
            column = 1;
            line++;
        }

        return c;
    }
    private bool Match(char expected)
    {
        if (IsAtEnd || _source?[current] != expected) return false;
        ++current;
        return true;
    }
    private void AddToken(TO token, object? literal = null)
    {
        tokens[tokenCount++] = new Token(token, _source[start..current], literal, line, column);
    }
    private bool IsAtEnd => current >= _source?.Length;

    private char Peek => IsAtEnd ? '\0' : _source?[current] ?? '\0';

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private char PeekNext()
    {
        if (current + 1 >= _source?.Length) return '\0';

        if(_source != null)
            return _source[current + 1];

        return '\0';
    }
    private static string DashLine() => string.Concat(Enumerable.Repeat('-', Console.WindowWidth));

    public static void Error(int line, int column, string label, string message, string lexeme, string? collated = "")
    {
        //HadError = true;
        string formattedError = $@"
                    An Error Occurred.
                    {DashLine()}
 
                    {label}: {message}
                    | {collated}
                    | Line {line - 1}, Column {column}
                    | : '{(lexeme)}'
                    {DashLine()}
         ";

        Console.Error.WriteLine(formattedError);
    }
}