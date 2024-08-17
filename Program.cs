using ExpressionEvaluator;

Console.Write("Expr: ");
string inputExpr = Console.ReadLine() ?? "";
inputExpr = String.Concat(inputExpr.Where(c => !Char.IsWhiteSpace(c)));

ExprParser parser = new(inputExpr);

string value = parser.Parse();

Console.WriteLine(value);
