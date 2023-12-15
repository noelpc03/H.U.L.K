

public class Parser
{
    List<Token> TokenList;
    Token actualToken;
    //object actualValue;
    int position;
    public Parser(List<Token> tokenList)
    {
        TokenList = tokenList;
        position = 0;
        actualToken = TokenList[position];
    }
    public AST Instruccion()
    {

        Instruccion node = new Instruccion(Not());
        if (actualToken.Type != TokenType.SEMICOLON)
        {
            Error.Syntax($"unexpected token:{actualToken.Type}. Expected: semicolon");
        }
        GetNextPosition();
        if (actualToken.Type != TokenType.EOF)
        {
            Error.Syntax($"unexpected token:{actualToken.Type}. Expected: EOF");
        }
        return node;

    }
    public AST Not()
    {
        while (actualToken.Type == TokenType.NOT)
        {
            GetNextPosition();

            return new UnaryOparator(AndOr(), TokenType.NOT);
        }
        return AndOr();

    }
    public AST AndOr()
    {
        AST node = Operator();
        if (actualToken.Type == TokenType.OR || actualToken.Type == TokenType.AND)
        {
            Token actual = actualToken;
            GetNextPosition();
            node = new BinaryOperator(node, actual, Operator());
        }
        return node;
    }
    public AST Operator()
    {
        AST node = SumAndRest();
        while (actualToken.Type == TokenType.EQUALS || actualToken.Type == TokenType.DIFFERENT || actualToken.Type == TokenType.GREATER || actualToken.Type == TokenType.GREATER_EQUAL || actualToken.Type == TokenType.LESS || actualToken.Type == TokenType.LESS_EQUAL)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.EQUALS)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.DIFFERENT)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.GREATER)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.GREATER_EQUAL)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.LESS)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.LESS_EQUAL)
            {
                GetNextPosition();
            }
            node = new BinaryOperator(node, j, SumAndRest());
        }
        return node;
    }

    public AST SumAndRest()
    {
        AST node = MultAndDiv();
        while (actualToken.Type == TokenType.PLUS || actualToken.Type == TokenType.MINUS || actualToken.Type == TokenType.CONCAT)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.PLUS)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.MINUS)
            {
                GetNextPosition();
            }
            else if (actualToken.Type == TokenType.CONCAT)
            {
                GetNextPosition();
            }
            node = new BinaryOperator(node, j, MultAndDiv());

        }
        return node;
    }
    public AST MultAndDiv()
    {
        AST node = Pow();
        while (actualToken.Type == TokenType.MULT || actualToken.Type == TokenType.DIV || actualToken.Type == TokenType.MOD)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.MULT)
            {
                GetNextPosition();
            }
            if (actualToken.Type == TokenType.DIV)
            {
                GetNextPosition();
            }
            if (actualToken.Type == TokenType.MOD)
            {
                GetNextPosition();
            }
            node = new BinaryOperator(node, j, Pow());
        }
        return node;
    }
    public AST Pow()
    {
        AST node = Factor();
        while (actualToken.Type == TokenType.POW)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.POW)
            {
                GetNextPosition();
            }
            node = new BinaryOperator(node, j, Factor());
        }
        return node;
    }

    public AST Factor()
    {
        AST node = new AST();

        switch (actualToken.Type)
        {
            case TokenType.SEN:
                return Sen();
            case TokenType.COS:
                return Cos();
            case TokenType.LOG:
                return Log();
            case TokenType.VAR:
                Token n = actualToken;
                GetNextPosition();
                node = new Var(n.Value);
                break;
            case TokenType.LET:
                return Declaration();
            case TokenType.FUNCTIONS:
                return Function();
            case TokenType.CallFunction:
                return CallFunction();
            case TokenType.PRINT:
                return Print();
            case TokenType.IF:
                return Conditional();
            case TokenType.NUMBER:
                Token j = actualToken;
                GetNextPosition();
                node = new Number(j.Value);
                break;
            case TokenType.PI:
                GetNextPosition();
                node = new Number(Math.PI);
                break;
            case TokenType.L_PARENT:
                GetNextPosition();
                node = Not();
                Test(TokenType.R_PARENT);
                break;
            case TokenType.STRING:
                Token k = actualToken;
                GetNextPosition();
                node = new String(k.Value);
                break;
            case TokenType.TRUE:
                Token l = actualToken;
                GetNextPosition();
                node = new Bool(l.Value);
                break;
            case TokenType.FALSE:
                Token m = actualToken;
                GetNextPosition();
                node = new Bool(m.Value);
                break;
            default:
                Error.Syntax($"unexpected token:{actualToken.Type}. Expected: EOF");
                break;

        }
        return node;
    }

    public AST Function()
    {
        Test(TokenType.FUNCTIONS);
        Token f = actualToken;
        Test(TokenType.VAR);
        Test(TokenType.L_PARENT);

        Dictionary<string, object> variable = new Dictionary<string, object>();
        if (actualToken.Type == TokenType.VAR)
        {
            variable.Add(actualToken.Value, 0);
            GetNextPosition();
            while (actualToken.Type == TokenType.COMMA)
            {
                Test(TokenType.COMMA);
                variable.Add(actualToken.Value, 0);
                GetNextPosition();
            }
        }

        Test(TokenType.R_PARENT);

        Test(TokenType.RETURN);

        FUNCTION ast = new FUNCTION(f, variable, Not());
        return ast;
    }


    public AST CallFunction()
    {
        Token f = actualToken;

        Test(TokenType.CallFunction);

        Test(TokenType.L_PARENT);

        List<AST> variable = new List<AST>() { Not() };

        while (actualToken.Type == TokenType.COMMA)
        {
            Test(TokenType.COMMA);
            variable.Add(Not());

        }


        Test(TokenType.R_PARENT);


        return new CallFUNCTION(f, variable);

    }
    public AST Declaration()
    {
        Test(TokenType.LET);
        Dictionary<string, AST> variables = new Dictionary<string, AST>();
        while (true)
        {
            string var = actualToken.Value;
            Test(TokenType.VAR);
            Test(TokenType.ASIGNATION);
            variables.Add(var, Not());

            if (actualToken.Type != TokenType.COMMA)
            {
                break;
            }
            GetNextPosition();
        }
        Test(TokenType.IN);
        AST node = Not();
        return new Declaration(node, variables);
    }
    public AST Sen()
    {
        GetNextPosition();
        Test(TokenType.L_PARENT);
        AST node = new Sen(Not());
        Test(TokenType.R_PARENT);
        return node;
    }
    public AST Cos()
    {
        GetNextPosition();
        Test(TokenType.L_PARENT);
        AST node = new Cos(Not());
        Test(TokenType.R_PARENT);
        return node;
    }
    public AST Log()
    {
        GetNextPosition();
        Test(TokenType.L_PARENT);
        AST node = Not();
        Test(TokenType.COMMA);
        AST expression = Not();
        Test(TokenType.R_PARENT);
        return new LOG(node, expression);
    }



    public AST Print()
    {
        GetNextPosition();
        Test(TokenType.L_PARENT);
        AST node = new Print(Not());
        Test(TokenType.R_PARENT);

        return node;
    }
    public AST Conditional()
    {
        Test(TokenType.IF);
        Test(TokenType.L_PARENT);
        AST ifstament = Not();
        Test(TokenType.R_PARENT);
        AST thenstament = new AST();
        if (actualToken.Type == TokenType.RETURN)
        {
            GetNextPosition();
            thenstament = Not();
        }
        else
        {
            thenstament = Not();
        }
        Test(TokenType.ELSE);
        AST elsestament = new Print(Not());
        return new Condicional(ifstament, thenstament, elsestament);
    }


    public void Test(TokenType type)
    {
        if (type == actualToken.GetType())
        {
            // actualValue = actualToken.Value;
            GetNextPosition();
        }
        else Error.Syntax($"unexpected token:{actualToken.Type}. Expected: {type}");
    }

    public void GetNextPosition()
    {
        position++;
        if (position < TokenList.Count()) actualToken = TokenList[position];
        else actualToken = new Token("", TokenType.EOF);
    }
}