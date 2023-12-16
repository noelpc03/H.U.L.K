Hulk.Main();

static class Hulk
{

    public static Dictionary<string, FUNCTION> Funciones = new Dictionary<string, FUNCTION>();

    public static void Main()
    {

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Presiona ENTER para continuar o ESC para salir");
        while (true)
        {

            if (Console.KeyAvailable)
            {

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(">");
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        Start(Console.ReadLine()!);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{ex.Message}. Try again please.");
                    }
                    // foreach (var item in Funciones)
                    // {
                    //     Console.WriteLine(item.Key);
                    // }
                }

                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }

        }

    }
    public static void Start(string entrance)
    {
        Lexer lexer = new Lexer(entrance);
        if (lexer.TokenList.Count != 0)
        {
            //Console.WriteLine(string.Join('\n', lexer.TokenList));
            Parser parser = new Parser(lexer.TokenList);
            Interpreter interpreter = new Interpreter(parser);
            Console.WriteLine(Convert.ToString(interpreter.Interpreter0()));
        }
    }



}