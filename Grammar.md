## Grammar

Bracketed expressions are difficult to deal with.  
The shunting-yard algorithm can deal with them, but only if you have
special handling that treats brackets as sort of pseudo-operators.  
Typically when you need to push an operator to the operator stack, you
first have to check if the head is of greater-or-equal presidence.
With a left-bracket, you just add it regardless. Then if you have an
operator of lower presidence, that just gets added to the stack as well
even though the brackets should have high presidence.

The shunting-yard algorithm is definitely possible (and more sensible
than what I'm about to suggest) but I think it woud be more fun to tap into
Formal Langage Grammars like a Compiler would.

```
Expr ::= Val {Operator Val}.

Operator ::= '+' | '-' | '*' | '/'.
Val ::= Bexpr | Num | '-' Num.

Bexpr ::= '(' Expr ')'.

Num ::= Digit [Digit].
Digit ::= '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9'.
```

We parse the input string character-by-character, validating the input
string against the grammar.

Whenever an expression or bracketed expression is complete, we can evaluate
it using a simple (no brackets) shunting-yard case.
In the case of bracketed expressions, we then place the value back into
the parent expression before evaluation.
