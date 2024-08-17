using System;

namespace ExpressionEvaluator;

internal enum ParserState
{
    NotChecked,
    Failed,
    Succeeded
}

internal class Parser
{
    protected string Input { get; set; }
    protected int Pointer { get; set; }
    public ParserState State { get; protected set; }

    public Parser(string input)
    {
        Input = input;
    }

    protected char CurrentChar() {
        if (Pointer >= Input.Length)
        {
            return '\0';
        }

        return Input[Pointer];
    }

    protected bool Check(char expectedChar) {
        return CurrentChar() == expectedChar;
    }

    protected bool CheckAny(List<char> expectedChars)
    {
        foreach (char expectedChar in expectedChars)
        {
            if (Check(expectedChar))
            {
                return true;
            }
        }

        return false;
    }

    protected bool Scan(char expectedChar)
    {
        bool result = Check(expectedChar);

        if (!result)
        {
            State = ParserState.Failed;
        }
        else
        {
            Pointer++;
        }

        return result;
    }

    protected void Scan()
    {
        Pointer++;
    }
}
