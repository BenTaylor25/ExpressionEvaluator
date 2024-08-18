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
        Pointer = 0;
        State = ParserState.NotChecked;
    }

    /// <summary>
    /// Use the input string and pointer to retreive the current character.
    /// Returns '\0' once pointer reaches the length of the input.
    /// </summary>
    protected char CurrentChar()
    {
        if (Pointer >= Input.Length)
        {
            return '\0';
        }

        return Input[Pointer];
    }

    /// <summary>
    /// Check that the current character meets the expected character.
    /// </summary>
    /// <returns>True if equal, false if not.</returns>
    protected bool Check(char expectedChar)
    {
        return CurrentChar() == expectedChar;
    }

    /// <summary>
    /// Pass a list of possible expected characters. Return true if any
    /// of them match current character.
    /// </summary>
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

    /// <summary>
    /// Check for expected character. If valid, increment pointer,
    /// else set State to Failed.
    /// </summary>
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

    /// <summary>
    /// Accept current value and move pointer forwards.
    /// </summary>
    protected void Scan()
    {
        Pointer++;
    }
}
