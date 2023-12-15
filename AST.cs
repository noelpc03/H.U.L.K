

using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

public class AST { }

public class Instruccion : AST
{

    public AST node;

    public Instruccion(AST node)
    {
        this.node = node;
    }
}

public class FUNCTION : AST
{
    public string name;
    public Dictionary<string, object> argumentos;
    public AST Statement;
    //public List<AST>arg;

    public FUNCTION(Token names, Dictionary<string, object> argumentos, AST Statement)
    {
        name = names.Value;
        this.argumentos = argumentos;
        this.Statement = Statement;
        Program2.Funciones.Add(names.Value, this);
    }


    /* public FUNCTIONAL(Token names,List<AST>arg){
         name=(string)names.Value;
         this.arg=arg;
     }
     */
}
public class CallFUNCTION : AST
{
    public string name;
    public List<AST> arg;
    public CallFUNCTION(Token names, List<AST> arg)
    {
        name = names.Value;
        this.arg = arg;
    }



}
public class BinaryOperator : AST
{
    public AST left;
    public AST right;
    public string signo;

    public BinaryOperator(AST left, Token signo, AST right)
    {
        this.left = left;
        this.right = right;
        this.signo = signo.Value.ToString()!;
    }
}
public class UnaryOparator : AST
{
    public AST right;
    public string signo;
    public UnaryOparator(AST right, object signo)
    {
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
public class String : AST
{
    public object Value;
    public String(object value)
    {
        this.Value = value;
    }
}
public class Bool : AST
{
    public object Value;

    public Bool(object value)
    {
        Value = value;
    }
}
public class Condicional : AST
{
    public AST ThenStatement;
    public AST IFStatement;
    public AST ElseStatement;

    public Condicional(AST IFStatement, AST ThenStatement, AST ElseStatement)
    {
        this.IFStatement = IFStatement;
        this.ElseStatement = ElseStatement;
        this.ThenStatement = ThenStatement;
    }
}

public class Declaration : AST
{
    public AST Statement;
    public Dictionary<string, AST> Variable;

    public Declaration(AST Statement, Dictionary<string, AST> variable)
    {
        Variable = variable;
        this.Statement = Statement;
    }
}
public class Var : AST
{
    public string Name;
    public Var(string name)
    {
        Name = name;
    }
}

public class Print : AST
{
    public AST Compound;// expresion que constituye una nueva lista de instrucciones
    public Print(AST compound)
    {
        Compound = compound;
    }
}
public class Sen : AST
{
    public AST Statement;

    public Sen(AST Statement)
    {
        this.Statement = Statement;
    }
}
public class Cos : AST
{
    public AST Statement;

    public Cos(AST Statement)
    {
        this.Statement = Statement;
    }
}

public class LOG : AST
{

    public AST bases;
    public AST Statement;
    public LOG(AST bases, AST Statement)
    {
        this.bases = bases;
        this.Statement = Statement;
    }
    public LOG(AST Statement)
    {
        this.Statement = Statement;
    }

}