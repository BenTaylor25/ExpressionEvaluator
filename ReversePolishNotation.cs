
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
        foreach (string s in infixExprTokens) Console.Write(s + ", ");
        Console.WriteLine();

        List<string> rpnExprTokens = InfixToRPN(infixExprTokens);
        foreach (string s in rpnExprTokens) Console.Write(s + ", ");
        Console.WriteLine();

        return infixExpr;
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

}
