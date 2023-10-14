

public abstract class NodeVisitor
{
    protected object Visit(AST node)
    {
        if (node is BinaryOperator)
        {
            return VisitBinaryOperator((BinaryOperator)node);
        }
        if (node is Number)
        {
            return VisitNum((Number)node);
        }
        return null!;
    }
    public abstract object VisitNum(Number node);
    public abstract object VisitBinaryOperator(BinaryOperator node);
}

public class Interpreter : NodeVisitor
{
    public Parser parser;
    public Interpreter(Parser parser)
    {
        this.parser = parser;
    }

    public object Interpreter0()
    {
        AST tree = parser.AndOr();
        return Visit(tree);
    }
    public override object VisitNum(Number node)
    {
        return node.Value;
    }
    public override object VisitBinaryOperator(BinaryOperator node)
    {
        object result = 0;

        object left = Visit(node.left);
        object right = Visit(node.right);

        if (node.signo == "+")
        {
            //(left is string )?(string)left:(left is bool)?(bool)left: Convert.ToSingle(left);
            result = Convert.ToSingle(left) + Convert.ToSingle(right);
        }

        else if (node.signo == "-")
        {
            result = Convert.ToSingle(left) - Convert.ToSingle(right);
        }

        else if (node.signo == "*")
        {
            result = Convert.ToSingle(left) * Convert.ToSingle(right);
        }

        else if (node.signo == "/")
        {
            result = Convert.ToSingle(left) / Convert.ToSingle(right);
        }
        else if (node.signo == "^")
        {
            result = Math.Pow(Convert.ToSingle(left), Convert.ToSingle(right));
        }
        else if (node.signo == "==")
        {
            result = (Convert.ToSingle(left) == Convert.ToSingle(right)) ? true : false;
        }
         else if (node.signo == "!=")
        {
            result = (Convert.ToSingle(left) != Convert.ToSingle(right)) ? true : false;
        }
         else if (node.signo == "<")
        {
            result = (Convert.ToSingle(left) < Convert.ToSingle(right)) ? true : false;
        }
         else if (node.signo == ">")
        {
            result = (Convert.ToSingle(left) > Convert.ToSingle(right)) ? true : false;
        }
         else if (node.signo == "<=")
        {
            result = (Convert.ToSingle(left) <= Convert.ToSingle(right)) ? true : false;
        }
         else if (node.signo == ">=")
        {
            result = (Convert.ToSingle(left) >= Convert.ToSingle(right)) ? true : false;
        }
         else if (node.signo == "|")
        {
            result = (Convert.ToBoolean(left) || Convert.ToBoolean(right)) ? true : false;
        }
         else if (node.signo == "&")
        {
            result = (Convert.ToBoolean(left) && Convert.ToBoolean(right)) ? true : false;
        }

        return result;
    }
}