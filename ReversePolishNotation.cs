
internal static class ReversePolishNotation
{
    private static readonly Dictionary<string, int> OPERATORS = new()
    {
        { "*", 2 },
        { "/", 2 },
        { "+", 1 },
        { "-", 1 }
    };

    public static string EvaluateExpression(string infixExpr)
    {
        List<string> infixExprTokens = TokeniseExpr(infixExpr);

        List<string> rpnExprTokens = InfixToRPN(infixExprTokens);

        string value = EvaluateRPN(rpnExprTokens);

        return value;
    }

    private static List<string> TokeniseExpr(string infixExpr)
    {
        List<string> Tokenised = new();

        string currentString = "";
        
        for (int i = 0; i < infixExpr.Length; i++)
        {
            // This would break if you were to add a multi-character operator.
            if (OPERATORS.ContainsKey(infixExpr[i].ToString()))
            {
                Tokenised.Add(currentString);
                currentString = "";

                Tokenised.Add(infixExpr[i].ToString());
            }
            else
            {
                currentString += infixExpr[i];
            }
        }

        Tokenised.Add(currentString);

        return Tokenised;
    }

    private static List<string> InfixToRPN(List<string> InfixTokens)
    {
        List<string> rpnTokens = new();
        Stack<string> operatorStack = new();

        foreach (string token in InfixTokens)
        {
            if (OPERATORS.ContainsKey(token))
            {
                int newPrecedence = OPERATORS[token];

                if (newPrecedence > GetHeadPrecedence(operatorStack))
                {
                    operatorStack.Push(token);
                }
                else
                {
                    while (newPrecedence <= GetHeadPrecedence(operatorStack))
                    {
                        rpnTokens.Add(operatorStack.Pop());
                    }

                    operatorStack.Push(token);
                }
            }
            else
            {
                rpnTokens.Add(token);
            }
        }

        while (operatorStack.Count > 0)
        {
            rpnTokens.Add(operatorStack.Pop());
        }

        return rpnTokens;
    }

    private static int GetHeadPrecedence(Stack<string> operatorStack)
    {
        int precedence = 0;

        if (operatorStack.Count > 0)
        {
            precedence = OPERATORS[operatorStack.Peek()];
        }

        return precedence;
    }

    private static string EvaluateRPN(List<string> rpnExprTokens)
    {
        Stack<double> valueStack = new();

        foreach (string token in rpnExprTokens)
        {
            if (token == "+")
            {
                double b = valueStack.Pop();
                double a = valueStack.Pop();

                valueStack.Push(a + b);
            }
            else if (token == "-")
            {
                double b = valueStack.Pop();
                double a = valueStack.Pop();

                valueStack.Push(a - b);
            }
            else if (token == "*")
            {
                double b = valueStack.Pop();
                double a = valueStack.Pop();

                valueStack.Push(a * b);
            }
            else if (token == "/")
            {
                double b = valueStack.Pop();
                double a = valueStack.Pop();

                valueStack.Push(a / b);
            }
            else
            {
                bool success = Double.TryParse(token, out double val);

                if (success)
                {
                    valueStack.Push(val);
                }
                else
                {
                    Console.WriteLine($"Failed to parse '{token}' as a double.");
                }
            }
        }

        if (valueStack.Count != 1)
        {
            Console.WriteLine("Value Stack Size not equal to 1 after RPN Evaluation");
        }

        double finalValue = valueStack.Pop();

        return finalValue.ToString();
    }
}
