namespace GalliumScript.Tokens;

public record Token(TokenType Type, string Lexeme, object? Literal, int Line, int Column);