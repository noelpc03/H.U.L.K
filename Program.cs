Program2.Main();

static class Program2
{

    public static Dictionary<string, FUNCTION> Funciones = new Dictionary<string, FUNCTION>();

    public static void Main()
    {

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("PRESIONA ENTER O ESC PARA ALGUNA FUNCION");
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
                    //Pincha(Console.ReadLine()!);
                    try
                    {
                        Pincha(Console.ReadLine()!);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}.Try again please.");
                    }
                    foreach (var item in Funciones)
                    {
                        Console.WriteLine(item.Key);
                    }
                }

                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }

        }


        static void Pincha(string entrance)
        {
            Lexer lexer = new Lexer(entrance);
            if (lexer.TokenList.Count != 0)
            {
                Console.WriteLine(string.Join('\n', lexer.TokenList));
                Parser parser = new Parser(lexer.TokenList);
                Interpreter interpreter = new Interpreter(parser);
                Console.WriteLine(Convert.ToString(interpreter.Interpreter0()));
            }
        }


    }
}