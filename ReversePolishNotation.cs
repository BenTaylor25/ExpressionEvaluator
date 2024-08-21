namespace ExpressionEvaluator;

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

    public struct EvaluateExpressionResult
    {
        public bool expressionIsValid;
        public string value;
    }

    /// <summary>
    /// Takes an unbracketed infix expression and returns its value
    /// in string form.
    /// </summary>
    public static EvaluateExpressionResult EvaluateExpression(
        string infixExpr
    )
    {
        List<string> infixExprTokens = TokeniseExpr(infixExpr);

        List<string> rpnExprTokens = InfixToRPN(infixExprTokens);

        EvaluateExpressionResult result = EvaluateRPN(rpnExprTokens);

        return result;
    }

    /// <summary>
    /// Splits infix expression into operators and operands.
    /// <br />
    /// e.g. `"1+274*3"` -> `["1", "+", "274", "*", "3"]`.
    /// </summary>
    private static List<string> TokeniseExpr(string infixExpr)
    {
        bool previousWasValue = false;
        List<string> Tokenised = new();

        string currentString = "";
        
        for (int i = 0; i < infixExpr.Length; i++)
        {
            // This would break if you were to add a
            // multi-character operator.
            if (OPERATORS.ContainsKey(infixExpr[i].ToString()))
            {
                // Unary minus.
                if (!previousWasValue && infixExpr[i] == '-')
                {
                    currentString += "0";
                }

                Tokenised.Add(currentString);
                currentString = "";
                previousWasValue = false;

                Tokenised.Add(infixExpr[i].ToString());
            }
            else
            {
                currentString += infixExpr[i];
                previousWasValue = true;
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

                while (
                    newPrecedence <= GetHeadPrecedence(operatorStack)
                )
                {
                    rpnTokens.Add(operatorStack.Pop());
                }

                operatorStack.Push(token);
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
    private static EvaluateExpressionResult EvaluateRPN(
        List<string> rpnExprTokens
    )
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
                    Logging.LogError(
                        $"Failed to parse '{token}' as a double."
                    );

                    return new()
                    {
                        expressionIsValid = false,
                        value = string.Empty
                    };
                }
            }
        }

        if (valueStack.Count != 1)
        {
            Logging.LogError(
                "Value Stack Size not equal to 1 after RPN Evaluation"
            );

            return new()
            {
                expressionIsValid = false,
                value = string.Empty
            };
        }

        double finalValue = valueStack.Pop();

        return new()
        {
            expressionIsValid = true,
            value = finalValue.ToString()
        };
    }
}
