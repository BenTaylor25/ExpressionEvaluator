
internal static class ReversePolishNotation
{
    /// <summary>
    /// Dictionary where keys are registered operators and their values
    /// are their relative precedences.
    /// </summary>
    private static readonly Dictionary<string, int> OPERATORS = new()
    {
        { "*", 2 },
        { "/", 2 },
        { "+", 1 },
        { "-", 1 }
    };

    /// <summary>
    /// Takes an unbracketed infix expression and returns its value
    /// in string form.
    /// </summary>
    public static string EvaluateExpression(string infixExpr)
    {
        List<string> infixExprTokens = TokeniseExpr(infixExpr);

        List<string> rpnExprTokens = InfixToRPN(infixExprTokens);

        string value = EvaluateRPN(rpnExprTokens);

        return value;
    }

    /// <summary>
    /// Splits infix expression into operators and operands.
    /// <br />
    /// e.g. `"1+274*3"` -> `["1", "+", "274", "*", "3"]`.
    /// </summary>
    private static List<string> TokeniseExpr(string infixExpr)
    {
        List<string> Tokenised = new();

        string currentString = "";
        
        for (int i = 0; i < infixExpr.Length; i++)
        {
            // This would break if you were to add a
            // multi-character operator.
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

    /// <summary>
    /// Sorts infix expression tokens into Reverse Polish Notation order.
    /// <br />
    /// e.g. `["11", "+", "2", "+", "3", "*", "4"]` -> <br />
    /// `["11", "2", "+", "3", "4", "*", "+"]`.
    /// </summary>
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
                    while (
                        newPrecedence <= GetHeadPrecedence(operatorStack)
                    )
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

    /// <summary>
    /// Helper function that returns the precedence for the operator at
    /// the head of the operatorStack.
    /// <br />
    /// If the stack is empty, return a precedence of 0.
    /// </summary>
    private static int GetHeadPrecedence(Stack<string> operatorStack)
    {
        int precedence = 0;

        if (operatorStack.Count > 0)
        {
            precedence = OPERATORS[operatorStack.Peek()];
        }

        return precedence;
    }

    /// <summary>
    /// Take a list of expression tokens in Reverse Polish Notation and
    /// evaluate the expression.
    /// </summary>
    private static string EvaluateRPN(List<string> rpnExprTokens)
    {
        Stack<double> valueStack = new();

        foreach (string token in rpnExprTokens)
        {
            if (OPERATORS.ContainsKey(token))
            {
                // This will need to be refactored in order to handle
                // operators that use more or less than 2 operands.

                double b = valueStack.Pop();
                double a = valueStack.Pop();

                switch (token)
                {
                    case "+":
                        valueStack.Push(a + b);
                        break;
                    case "-":
                        valueStack.Push(a - b);
                        break;
                    case "*":
                        valueStack.Push(a * b);
                        break;
                    case "/":
                        valueStack.Push(a / b);
                        break;
                    default:
                        Console.WriteLine(
                            $"Encountered unhandled operator '{token}'."
                        );
                        break;
                }
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
                    Console.WriteLine(
                        $"Failed to parse '{token}' as a double."
                    );
                }
            }
        }

        if (valueStack.Count != 1)
        {
            Console.WriteLine(
                "Value Stack Size not equal to 1 after RPN Evaluation"
            );
        }

        double finalValue = valueStack.Pop();

        return finalValue.ToString();
    }
}
