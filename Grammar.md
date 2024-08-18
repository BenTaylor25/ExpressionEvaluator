## Grammar

The shunting-yard algorithm is definitely possible (and more sensible
than what I'm about to suggest) but I think it woud be more fun to tap into
Formal Langage Grammars like a Compiler would.

<a href='https://en.wikipedia.org/wiki/Formal_grammar'>Formal Grammars</a>
are a way of representing different components in a block of data.
Components can be comprised of other components, and we can scan through
the data programmatically based on what we expect to see.

If, while we're scanning through a string, we see a token that is invalid,
we know that the input is invalid.

I have used EBNF syntax to describe the grammar used in this solution:
```
Expr ::= Val {Operator Val}.

Operator ::= '+' | '-' | '*' | '/'.
Val ::= Bexpr | Num.

Bexpr ::= '(' Expr ')'.

Num ::= ['-'] Digit {Digit}.
Digit ::= '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9'.
```

We parse the input string character-by-character, validating the input
string against the grammar.

Whenever an expression or bracketed expression is complete, we can evaluate
it using a simple (no brackets) shunting-yard case.
In the case of bracketed expressions, we then place the value back into
the parent expression before evaluation.
