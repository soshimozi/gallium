using GalliumScript.Expressions;
using GalliumScript.Tokens;
using System.Security.Claims;
using GalliumScript.Statements;

namespace GalliumScript.Extras;

internal static class Utils
{
    public const char NOTHING = ' ';

    public static string SoftType(object? obj) => obj switch
    {
        double => "number",
        string => "string",
        Expr => "expression",
        object[] => "list",
        object => "object",
        null => "null",
    };

    public static string? Stringify(object? obj)
    {
        if (obj is double)
        {
            string text = obj.ToString()!;
            return text.EndsWith(".0", StringComparison.InvariantCulture) ? text[..^2] : text;
        }
        if (obj is Token to)
        {
            return to.Literal?.ToString();
        }

        return obj?.ToString();
    }

    /// <summary>
    /// Performs action if cls.superclass is not null
    /// </summary>
    /// <param name="cls">Class</param>
    /// <param name=""></param>
    public static void IfSuperclass(this ClassStatement cls, Action action)
    {
        if (cls.Superclass is not null)
        {
            action();
        }
    }
}