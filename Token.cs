public class Token
{
    public TokenType Type { get; private set; }
    public string Value { get; private set; }

    public Token(string value, TokenType type)
    {
        this.Value = value;
        this.Type = type;
    }
    public override string ToString() => $"{Type}: {Value}";

    public TokenType GetType()=> Type;


}












