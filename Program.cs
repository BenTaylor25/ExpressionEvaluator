using ExpressionEvaluator;

Console.WriteLine("Expression Evaluator");
Console.WriteLine("--------------------");

while (true)
{
    Console.Write("Expr: ");
    string inputExpr = Console.ReadLine() ?? "";
    inputExpr = String.Concat(inputExpr.Where(c => !Char.IsWhiteSpace(c)));

    if (Constants.CLI_EXIT_KEYWORDS.Contains(inputExpr))
    {
        break;
    }

    ExprParser parser = new(inputExpr);

    string value = parser.Parse();

    if (parser.State == ParserState.Succeeded)
    {
        Console.WriteLine(value);
    }
    else
    {
        Console.WriteLine("Expression is invalid.");
    }
    Console.WriteLine();
}
