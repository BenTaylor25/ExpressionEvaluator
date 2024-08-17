using ExpressionEvaluator;

Console.Write("Expr: ");
string inputExpr = Console.ReadLine() ?? "";
inputExpr = String.Concat(inputExpr.Where(c => !Char.IsWhiteSpace(c)));

ExprParser parser = new(inputExpr);

parser.Parse();

Console.WriteLine(parser.State.ToString());
