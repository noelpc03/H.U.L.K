

using System.Security.AccessControl;

public class AST
{

}

public class BinaryOperator : AST
{
    public AST left;
    public AST right;
    public string signo;

    public BinaryOperator(AST left, object signo, AST right)
    {
        this.left = left;
        this.right = right;
        this.signo = signo.ToString()!;
    }
}

public class Number : AST
{
    public object Value;

    public Number(object Value)
    {
        this.Value = Value;
    }
}
public class Condicional : AST
{
    public AST Statement;
    public AST IFStatement;
    public AST ElseStatement;

    public Condicional(AST IFStatement, AST Statement, AST ElseStatement)
    {
        this.IFStatement = IFStatement;
        this.ElseStatement = ElseStatement;
        this.Statement = Statement;
    }
}

public class Declaration : AST
{
    public AST Statement;
    public List<AST> Variable;

    public Declaration(AST Statement, List<AST> Variable)
    {
        this.Variable = Variable;
        this.Statement = Statement;
    }
}

public class Print : AST
{

}