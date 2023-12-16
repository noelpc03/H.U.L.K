
public abstract class NodeVisitor
{
    protected object Visit(AST node) // metodo que evalua el arbol en dependencia de su tipo
    {
        if (node is BinaryOperator)
        {
            return VisitBinaryOperator((BinaryOperator)node);
        }
        if (node is CallFUNCTION)
        {
            return VisitCallFunctiones((CallFUNCTION)node);
        }
        if (node is Number)
        {
            return VisitNum((Number)node);
        }
        if (node is UnaryOparator)
        {
            return VisitUnaryOparator((UnaryOparator)node);
        }
        if (node is String)
        {
            return VisitString((String)node);
        }
        if (node is Bool)
        {
            return VisitBoolean((Bool)node);
        }
        if (node is Print)
        {
            return VisitPrint((Print)node);
        }
        if (node is Condicional)
        {
            return VisitConditional((Condicional)node);
        }
        if (node is Sen)
        {
            return VisitSen((Sen)node);
        }
        if (node is Cos)
        {
            return VisitCos((Cos)node);
        }
        if (node is LOG)
        {
            return VisitLog((LOG)node);
        }
        if (node is Declaration)
        {
            return VisitDeclaration((Declaration)node);
        }
        if (node is Var)
        {
            return VisitVar((Var)node);
        }
        if (node is Instruccion)
        {
            return VisitInstruccion((Instruccion)node);
        }
        return null!;
    }
    public abstract object VisitNum(Number node);
    public abstract object VisitBinaryOperator(BinaryOperator node);
    public abstract object VisitUnaryOparator(UnaryOparator node);
    public abstract object VisitString(String node);
    public abstract object VisitBoolean(Bool node);
    public abstract object VisitConditional(Condicional node);
    public abstract object VisitPrint(Print node);
    public abstract object VisitSen(Sen node);
    public abstract object VisitCos(Cos node);
    public abstract object VisitLog(LOG node);
    public abstract object VisitDeclaration(Declaration node);
    public abstract object VisitVar(Var node);
    public abstract object VisitCallFunctiones(CallFUNCTION node);
    public abstract object VisitInstruccion(Instruccion node);
}

public class Interpreter : NodeVisitor
{
    double FuntionCalls;
    public Parser parser;
    public Interpreter(Parser parser)
    {
        this.parser = parser;
        Scope = new Dictionary<string, object>();
        FuntionCalls = 0;
    }
    public Dictionary<string, object> Scope;

    public object Interpreter0()
    {
        AST tree = parser.Instruccion();
        //Console.WriteLine(tree);
        return Visit(tree);
    }
    public override object VisitInstruccion(Instruccion node)
    {
        return Visit(node.node);
    }

    public override object VisitCallFunctiones(CallFUNCTION node)
    {

        Dictionary<string, object> dic = new Dictionary<string, object>(Scope);
        FuntionCalls++;
        if (FuntionCalls >= 500)
        {
            throw new Exception("RecursionError: maximum resursion number exceeded");
        }
        if (Hulk.Funciones.ContainsKey(node.name))
        {
            int i = 0;

            Dictionary<string, object> local = new Dictionary<string, object>(Hulk.Funciones[node.name].argumentos);
            // se crea un diccionario local para modificar los valores de local sin cambiar los de la variable estatica


            if (local.Count != node.arg.Count)
            {
                Error.Semantic($"Function \" {node.name}\" receives {local.Count} argument(s), but {node.arg.Count} were gives");
            }

            foreach (KeyValuePair<string, object> item in local)
            {
                object g = Visit(node.arg[i]);
                if (g is null)
                {
                    Error.Semantic($"Empty args of the \"{node.name} \" function have been detected");
                    break;
                }
                local[item.Key] = g;
                i += 1;

            }
            Scope = local;

            object tree = Visit(Hulk.Funciones[node.name].Statement);
            Scope = dic;
            return tree;
        }


        return 0;
    }
    public override object VisitNum(Number node)
    {
        return Convert.ToDouble(node.Value);
    }
    public override object VisitString(String node)
    {
        return Convert.ToString(node.Value)!;
    }
    public override object VisitUnaryOparator(UnaryOparator node)
    {
        object right = Visit(node.right);
        if (right is bool x)
        {
            return !x;
        }
        else if (right is double)
        {
            Error.Semantic($"Unary Operator \"! \" cannot be used with number");
        }
        Error.Semantic($"Unary Operator \"! \" cannot be used with string");
        throw new Exception();

    }
    public override object VisitBoolean(Bool node)
    {
        return Convert.ToBoolean(node.Value);
    }
    public override object VisitConditional(Condicional node)
    {
        if (Visit(node.IFStatement) is bool x)
        {
            if (Convert.ToBoolean(x))
            {
                return Visit(node.ThenStatement);
            }
            return Visit(node.ElseStatement);
        }
        Error.Semantic($"\"if statement\" is not bool");
        throw new Exception();

    }
    public override object VisitPrint(Print node)
    {
        return Visit(node.Compound);
    }
    public override object VisitSen(Sen node)
    {
        if (Visit(node.Statement) is double x)
        {
            return Math.Sin(x);
        }
        Error.Semantic($"you can not use \"sen\" with a non doble expression");
        throw new Exception();

    }
    public override object VisitCos(Cos node)
    {
        if (Visit(node.Statement) is double x)
        {
            return Math.Cos(x);
        }
        Error.Semantic($"you can not use \"cos\" with a non doble expression");
        throw new Exception();
    }
    public override object VisitLog(LOG node)
    {
        if (node.bases is null && node.Statement is null)
        {
            Error.Semantic("The log function takes at least one arg.");
        }
        object tree = Visit(node.Statement!);
        if (tree is string || tree is bool)
        {
            Error.Semantic("The args of logarithm function is a double variable");
        }

        if (Convert.ToDouble(tree) <= 0)
        {
            Error.Semantic("The arg of the logarithm function is less than 0");
        }

        if (!(node.bases is null))
        {
            object item = Visit(node.bases);
            if (!(item is double))
            {
                Error.Semantic("The base of logarithm is a double variable");
            }

            if (Convert.ToDouble(item) <= 0 || Convert.ToDouble(item) == 1)
            {
                Error.Semantic("Logarithm to base less 0 or 1 is not defined");
            }

            return Math.Log(Convert.ToDouble(tree)) / Math.Log(Convert.ToDouble(item));
        }
        return Math.Log(Convert.ToDouble(tree));
    }

    public override object VisitDeclaration(Declaration node)
    {
        int count = Scope.Count();
        foreach (var item in node.Variable)
        {
            Console.WriteLine(item.Key);
            Scope.Add(item.Key, Visit(item.Value));
        }
        object output = Visit(node.Statement);
        while (Scope.Count() > count) Scope.Remove(Scope.Keys.Last());
        return output;
    }
    public override object VisitVar(Var node)
    {
        if (Scope.ContainsKey(node.Name))
        {
            return Scope[node.Name];
        }
        // devolver error
        Error.Semantic($"Varible \"{node.Name}\" was not found");
        throw new Exception();
    }
    public override object VisitBinaryOperator(BinaryOperator node)
    {
        object result = 0;

        object left = Visit(node.left);
        object right = Visit(node.right);
        if (node.signo == "+")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) + Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \"+ \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }

        else if (node.signo == "-")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) - Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \"- \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }

        else if (node.signo == "*")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) * Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \"*\" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }

        else if (node.signo == "/")
        {
            if (left is double && right is double)
            {
                if (Convert.ToDouble(right) != 0)
                {
                    result = Convert.ToDouble(left) / Convert.ToDouble(right);
                }
                else throw new Exception("\"/\" by 0 is not defined");
            }
            else Error.Semantic($"Operator \"/ \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "^")
        {
            if (left is double && right is double)
            {
                result = Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right));
            }
            else Error.Semantic($"Operator \"^ \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");

        }
        else if (node.signo == "==")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) == Convert.ToDouble(right);
            }

            else if (left is string && right is string)
            {
                result = Convert.ToString(left) == Convert.ToString(right);
            }
            else Error.Semantic($"Operator \"==\" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "!=")
        {
            if (left is string && right is string)
            {
                result = Convert.ToString(left) != Convert.ToString(right);
            }
            else Error.Semantic($"Operator \"!=\" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "<")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) < Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \"< \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == ">")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) > Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \"> \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "<=")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) <= Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \"<= \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == ">=")
        {
            if (left is double && right is double)
            {
                result = Convert.ToDouble(left) >= Convert.ToDouble(right);
            }
            else Error.Semantic($"Operator \">= \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "|")
        {
            if (left is bool && right is bool)
            {
                result = Convert.ToBoolean(left) || Convert.ToBoolean(right);
            }
            else Error.Semantic($"Operator \"| \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "&")
        {
            if (left is bool && right is bool)
            {
                result = Convert.ToBoolean(left) && Convert.ToBoolean(right);
            }
            else Error.Semantic($"Operator \"& \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "@")
        {
            if (left is not bool && right is not bool)
            {
                result = Convert.ToString(left) + Convert.ToString(right);
            }
            else Error.Semantic($"Operator \"@ \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }
        else if (node.signo == "%")
        {
            if (left is double && right is double)
            {
                if (Convert.ToDouble(right) != 0)
                {
                    result = Convert.ToDouble(left) % Convert.ToDouble(right);
                }
                else throw new Exception("\"%\" by 0 is not defined");
            }
            else Error.Semantic($"Operator \"% \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
        }

        return result;
    }
}