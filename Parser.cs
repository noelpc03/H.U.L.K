

using System.Linq.Expressions;
using System.Security;
using System.Xml.XPath;

public class Parser
{
    List<Token> TokenList;
    Token actualToken;
    object actualValue;
    int position;
    public Parser(List<Token> tokenList)
    {
        TokenList = tokenList;
        position = 0;
        actualToken = TokenList[position];
        //Console.WriteLine(SumAndRest());
        //ParseExpretion(tokenList); 
    }
    public AST AndOr()
    {
        AST node = Operator();
        while (actualToken.Type == TokenType.OR || actualToken.Type == TokenType.AND)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.OR)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.AND)
            {
                Test(actualToken.Type);
            }
            node = new BinaryOperator(node, j.Value, Operator());
        }
        return node;
    }  
    public AST Operator()
    {
        AST node = SumAndRest();
        while (actualToken.Type == TokenType.EQUALS||actualToken.Type == TokenType.DIFFERENT||actualToken.Type == TokenType.GREATER||actualToken.Type == TokenType.GREATER_EQUAL||actualToken.Type == TokenType.LESS||actualToken.Type == TokenType.LESS_EQUAL)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.EQUALS)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.DIFFERENT)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.GREATER)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.GREATER_EQUAL)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.LESS)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.LESS_EQUAL)
            {
                Test(actualToken.Type);
            }
            node = new BinaryOperator(node, j.Value, SumAndRest());
        }
        return node;
    }
    public AST SumAndRest()
    {
        AST node = MultAndDiv();
        while (actualToken.Type == TokenType.PLUS || actualToken.Type == TokenType.MINUS)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.PLUS)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.MINUS)
            {
                Test(actualToken.Type);
            }
            node = new BinaryOperator(node, j.Value, MultAndDiv());

        }
        return node;
    }
    public AST MultAndDiv()
    {
        AST node = Pow();
        while (actualToken.Type == TokenType.MULT || actualToken.Type == TokenType.DIV)
        {
            Token j = actualToken;
            if (actualToken.Type == TokenType.MULT)
            {
                Test(actualToken.Type);
            }
            if (actualToken.Type == TokenType.DIV)
            {
                Test(actualToken.Type);
            }
            node = new BinaryOperator(node, j.Value, Pow());
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
                Test(actualToken.Type);
            }
            node = new BinaryOperator(node, j.Value, Factor());
        }
        return node;
    }
        public AST Factor()
    {
        AST node = new AST();

        switch (actualToken.Type)
        {
            case TokenType.NUMBER:
                Token j = actualToken;
                Test(TokenType.NUMBER);
                node = new Number(j.Value);
                break;
            case TokenType.L_PARENT:
                Test(TokenType.L_PARENT);
                node = AndOr();
                Test(TokenType.R_PARENT);
                break;
        }
        return node;
    }

    public void Test(TokenType type)
    {
        if (type == actualToken.GetType())
        {
            actualValue = actualToken.Value;
            GetNextPosition();
        }
        else Console.WriteLine("Error");
    }

    public void GetNextPosition()
    {
        position++;
        if (position < TokenList.Count()) actualToken = TokenList[position];
    }
    /*
        public object ParseExpretion(List<Token> TokenList)
        {
            object result = Suma();
            return result;
        }
        public object Suma()
        {
            object result = Num();
            while (actualToken.Type == TokenType.PLUS)
            {
                Test(TokenType.PLUS);
                object temp = Num();
                result = int.Parse(result.ToString()!) + int.Parse(temp.ToString()!);
                GetNextPosition();
            }
            return result;

        }
        public object Num()
        {
            Test(TokenType.NUMBER);
            return actualValue;
        }
        public void Test(TokenType type)
        {
            if (type == actualToken.GetType())
            {
                actualValue = actualToken.Value;
                GetNextPosition();
            }
            else System.Console.WriteLine("Error");
        }

        public void GetNextPosition()
        {
            position++;
            if (position < TokenList.Count()) actualToken = TokenList[position];
        }



    */













}