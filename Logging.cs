namespace ExpressionEvaluator;

internal static class Logging
{
    public static void LogError(string message)
    {
        if (Constants.DEBUG_MODE)
        {
            Console.WriteLine(message);
        }
    }
}
