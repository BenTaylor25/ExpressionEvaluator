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
    private void Expr()
    {
        Val();

        while (CheckAny(OPERATOR_TERMINAL_START))
        {
            Operator();
            Val();
        }
    }

    /// <summary>
    /// Val ::= Bexpr | Num.
    /// </summary>
    private void Val()
    {
        if (CheckAny(BEXPR_TERMINAL_START))
        {
            Bexpr();
        }
        else
        {
            Num();
        }
    }

    /// <summary>
    /// Operator ::= '+' | '-' | '*' | '/'.
    /// </summary>
    private void Operator()
    {
        if (CheckAny(OPERATOR_TERMINAL_START))
        {
            Scan();
        }
    }

    /// <summary>
    /// Bexpr ::= '(' Expr ')'.
    /// </summary>
    private void Bexpr()
    {
        Scan('(');
        Expr();
        Scan(')');
    }

    /// <summary>
    /// Num ::= ['-'] Digit {Digit}.
    /// </summary>
    private void Num()
    {
        if (Check('-'))
        {
            Scan('-');
        }

        if (!CheckAny(DIGITS))
        {
            State = ParserState.Failed;
        }

        while (CheckAny(DIGITS))
        {
            Scan();
        }
    }
}
