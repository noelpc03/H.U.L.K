using System.IO.Compression;
using System.Text.RegularExpressions;

public class Lexer
{
    public List<string> entrance = new List<string>();
    public List<Token> TokenList = new List<Token>();

    public Lexer(string text)
    {
        Fill(text, entrance);
        Tokenizer(entrance, TokenList);
    }

    private static bool Point(string element)
    {
        int count = 0;
        for (int i = 0; i < element.Length; i++)
        {
            if (element[i] == '.') count++;
        }
        if (count > 1) return false;
        return true;
    }
    private static bool NoLetter(string element)
    {
        for (int i = 0; i < element.Length; i++)
        {
            if (char.IsLetter(element[i])) return false;
        }
        return true;
    }


    public static void Fill(string entrada, List<string> TokenList)
    {
        if (entrada.Length == 0) return;

        if (entrada[0] == '"')
        {
            int count = 0;
            while (true)
            {
                count++;
                if (count == entrada.Length)
                {
                    TokenList.Add(entrada);
                    break;
                }
                if (entrada[count] == '"')
                {
                    TokenList.Add(entrada.Substring(0, count + 1));
                    Fill(entrada.Substring(count + 1), TokenList);
                    break;

                }
            }
            return;
        }
        if (char.IsDigit(entrada[0]))
        {
            int count = 0;
            while (true)
            {
                count++;
                if (count == entrada.Length)
                {
                    TokenList.Add(entrada);
                    break;
                }
                if (entrada[count] == ' ')
                {
                    TokenList.Add(entrada.Substring(0, count));
                    Fill(entrada.Substring(count + 1), TokenList);
                    break;
                }
                else if (Regex.IsMatch(entrada[count].ToString(), @"^[^a-zA-Z0-9.]$"))
                {
                    TokenList.Add(entrada.Substring(0, count));
                    Fill(entrada.Substring(count), TokenList);
                    break;
                }
            }
            return;
        }
        if (char.IsLetter(entrada[0]))
        {
            int count = 0;
            while (true)
            {
                count++;
                if (count == entrada.Length)
                {
                    TokenList.Add(entrada);
                    break;
                }
                if (entrada[count] == ' ')
                {
                    TokenList.Add(entrada.Substring(0, count));
                    Fill(entrada.Substring(count + 1), TokenList);
                    break;
                }
                else if (Regex.IsMatch(entrada[count].ToString(), @"^[^a-zA-Z0-9_]$"))
                {
                    TokenList.Add(entrada.Substring(0, count));
                    Fill(entrada.Substring(count), TokenList);
                    break;
                }
            }
            return;
        }
        if (entrada[0] == ' ')
        {
            if (entrada.Length == 1) return;
            Fill(entrada.Substring(1), TokenList);
            return;
        }
        if (Regex.IsMatch(entrada[0].ToString(), @"[*+-/%|&=!<>@()^]"))
        {
            TokenList.Add(entrada[0].ToString());
            Fill(entrada.Substring(1), TokenList);
            return;
        }
        TokenList.Add(entrada[0].ToString());
        Fill(entrada.Substring(1), TokenList);
    }
    public static void Tokenizer(List<string> elements, List<Token> TokenList)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i][0] == '"')
            {
                if (elements[i][elements[i].Length - 1] == '"') TokenList.Add(new Token(elements[i], TokenType.STRING));
                else Console.WriteLine($"{elements[i]} invalid token");
            }

            else if (char.IsLetter(elements[i][0]))
            {
                if (elements[i] != "if" && elements[i] != "then" && elements[i] != "else" && elements[i] != "true" && elements[i] != "false" && elements[i] != "let" && elements[i] != "in") TokenList.Add(new Token(elements[i], TokenType.VAR));
                switch (elements[i])
                {
                    case "if":
                        TokenList.Add(new Token(elements[i], TokenType.IF));
                        break;
                    case "else":
                        TokenList.Add(new Token(elements[i], TokenType.ELSE));
                        break;
                    case "true":
                        TokenList.Add(new Token(elements[i], TokenType.TRUE));
                        break;
                    case "false":
                        TokenList.Add(new Token(elements[i], TokenType.FLASE));
                        break;
                    case "let":
                        TokenList.Add(new Token(elements[i], TokenType.LET));
                        break;
                    case "in":
                        TokenList.Add(new Token(elements[i], TokenType.IN));
                        break;
                    case "print":
                        TokenList.Add(new Token(elements[i], TokenType.PRINT));
                        break;

                }
            }

            else if (char.IsDigit(elements[i][0]))
            {
                if (NoLetter(elements[i]) && Point(elements[i])) TokenList.Add(new Token(elements[i], TokenType.NUMBER));
                else Console.WriteLine($"{elements[i]} invalid token");
            }

            else if (Regex.IsMatch(elements[i].ToString(), @"[*+-/%|&=!<>@()^;]"))
            {
                switch (elements[i])
                {
                    case "+":
                        TokenList.Add(new Token(elements[i], TokenType.PLUS));
                        break;
                    case "-":
                        TokenList.Add(new Token(elements[i], TokenType.MINUS));
                        break;
                    case "/":
                        TokenList.Add(new Token(elements[i], TokenType.DIV));
                        break;
                    case "%":
                        TokenList.Add(new Token(elements[i], TokenType.MOD));
                        break;
                    case "|":
                        TokenList.Add(new Token(elements[i], TokenType.OR));
                        break;
                    case "&":
                        TokenList.Add(new Token(elements[i], TokenType.AND));
                        break;
                    case "@":
                        TokenList.Add(new Token(elements[i], TokenType.CONCAT));
                        break;
                    case "(":
                        TokenList.Add(new Token(elements[i], TokenType.L_PARENT));
                        break;
                    case ")":
                        TokenList.Add(new Token(elements[i], TokenType.R_PARENT));
                        break;
                    case "^":
                        TokenList.Add(new Token(elements[i], TokenType.POW));
                        break;
                    case ";":
                        TokenList.Add(new Token(elements[i], TokenType.SEMICOLON));
                        break;
                    case "*":
                        TokenList.Add(new Token(elements[i], TokenType.MULT));
                        break;
                }

                if (elements[i] == "!")
                {
                    if ((elements.Count > i + 1) && (elements[i + 1] == "="))
                    {
                        TokenList.Add(new Token(elements[i] + elements[i + 1], TokenType.DIFFERENT));
                        i++;
                    }
                    else TokenList.Add(new Token(elements[i], TokenType.DIFFERENT));
                }
                else if (elements[i] == ">")
                {
                    if ((elements.Count > i + 1) && (elements[i + 1] == "="))
                    {
                        TokenList.Add(new Token(elements[i] + elements[i + 1], TokenType.GREATER_EQUAL));
                        i++;
                    }
                    else TokenList.Add(new Token(elements[i], TokenType.GREATER));
                }
                else if (elements[i] == "<")
                {
                    if ((elements.Count > i + 1) && (elements[i + 1] == "="))
                    {
                        TokenList.Add(new Token(elements[i] + elements[i + 1], TokenType.LESS_EQUAL));
                        i++;
                    }
                    else TokenList.Add(new Token(elements[i], TokenType.LESS));
                }
                else if (elements[i] == "=")
                {
                    if ((elements.Count > i + 1) && (elements[i + 1] == "="))
                    {
                        TokenList.Add(new Token(elements[i] + elements[i + 1], TokenType.EQUALS));
                        i++;
                    }
                    else TokenList.Add(new Token(elements[i], TokenType.ASIGNATION));
                }

            }
            else Console.WriteLine($"{elements[i]}no es valido");
        }
    }


}