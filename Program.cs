using ExpressionEvaluator;

Console.Write("Expr: ");
string inputExpr = Console.ReadLine() ?? "";
inputExpr = String.Concat(inputExpr.Where(c => !Char.IsWhiteSpace(c)));

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
