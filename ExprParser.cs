namespace ExpressionEvaluator;

internal class ExprParser : Parser
{
    private readonly List<char> EXPR_TERMINAL_START =
        new() { '(', '-', /* DIGITS */ };

    private readonly List<char> OPERATOR_TERMINAL_START =
        new(){ '+', '-', '*', '/' };

    private readonly List<char> VAL_TERMINAL_START =
        new() { '(', '-' /* DIGITS */ };

    private readonly List<char> BEXPR_TERMINAL_START =
        new() { '(' };

    private readonly List<char> DIGITS =
        new(){ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    public ExprParser(string inputExpr) : base(inputExpr)
    {
        EXPR_TERMINAL_START.AddRange(DIGITS);
        VAL_TERMINAL_START.AddRange(DIGITS);
    }

    public void Parse()
    {
        Expr();

        if (State == ParserState.NotChecked && Check('\0'))
        {
            State = ParserState.Succeeded;
        }
        else
        {
            State = ParserState.Failed;
        }
    }

    /// <summary>
    /// Expr ::= Val {Operator Val}.
    /// </summary>
    private string Expr()
    {
        string exprString = "";
        exprString += Val();

        while (CheckAny(OPERATOR_TERMINAL_START))
        {
            exprString += Operator();
            exprString += Val();
        }

        Console.WriteLine(exprString);
        return ReversePolishNotation.EvaluateExpression(exprString);
    }

    /// <summary>
    /// Val ::= Bexpr | Num.
    /// </summary>
    private string Val()
    {
        if (CheckAny(BEXPR_TERMINAL_START))
        {
            return Bexpr();
        }
        else
        {
            return Num();
        }
    }

    /// <summary>
    /// Operator ::= '+' | '-' | '*' | '/'.
    /// </summary>
    private string Operator()
    {
        if (CheckAny(OPERATOR_TERMINAL_START))
        {
            string operatorString = CurrentChar().ToString();
            Scan();
            return operatorString;
        }

        return "";
    }

    /// <summary>
    /// Bexpr ::= '(' Expr ')'.
    /// </summary>
    private string Bexpr()
    {
        Scan('(');
        string bexprString = Expr();
        Scan(')');

        return bexprString;
    }

    /// <summary>
    /// Num ::= ['-'] Digit {Digit}.
    /// </summary>
    private string Num()
    {
        string numString = "";

        if (Check('-'))
        {
            Scan('-');
            numString += '-';
        }

        if (!CheckAny(DIGITS))
        {
            State = ParserState.Failed;
        }

        while (CheckAny(DIGITS))
        {
            numString += CurrentChar();
            Scan();
        }

        return numString;
    }
}
