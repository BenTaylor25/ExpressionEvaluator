namespace ExpressionEvaluator;

internal static class Constants
{
    // Only not an explicit const so that my IDE doesn't complain
    // that there is unreachable code.
    public static readonly bool DEBUG_MODE = false;

    public static readonly List<string> CLI_EXIT_KEYWORDS = new()
    {
        "exit",
        "quit",
        "close",
        "x",
        "q"
    };
}
