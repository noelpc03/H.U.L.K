using System.Diagnostics;

public static class Error
{
    public static void Lexical(string item) // muestra error lexico
    {
        throw new Exception($"!lexical error: {item} is not a valid token");
    }
    public static void Syntax(string msg) // muestra error sintactico
    {
        throw new Exception("!syntax error:"+ msg);
    }
    public static void Semantic(string msg) // muestra error semantico
    {
        throw new Exception("!semantic error:"+msg);
    }
}