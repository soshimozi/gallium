namespace GalliumScript.Tokens;

public enum TokenType
{
    // Special Characters
    COMMA,

    DOT,
    SEMICOLON,
    HASH,       // #
    COLON,
    QUESTION,

    // Braces and Brackets
    LEFTPAREN,
    RIGHTPAREN,
    LEFTBRACE,
    RIGHTBRACE,
    LEFTbRACKET,
    RIGHTBRACKET,

    // Binary Operators
    SLASH,

    STAR,
    PERCENT,
    CARROT,
    BAR,        // |
    AMP,        // &


    // Comparison Tokens
    BANG, BANGEQUAL, // !, !=

    EQUAL, EQUALEQUAL,
    GREATER, GREATEREQUAL, RSHIFT,
    LESS, LESEQUAL, LSHIFT,

    // Arithmetic Operators
    PLUS,
    MINUS,

    // Literals
    IDENTIFIER,

    CHARACTERS,
    NUMBER,
    BOOL,

    // Keywords
    AND,

    CLASS,
    DEL,
    ELSE,
    FALSE,
    FUN,
    FOR,
    IF,
    NULL,
    OR,
    PRINT,
    RETURN,
    SUPER,
    THIS,
    TRUE,
    VAR,
    WHILE,
    ASSERT,
    BREAK,
    CONTINUE,

    // Special Tokens
    NEWLINE,

    EOF
}