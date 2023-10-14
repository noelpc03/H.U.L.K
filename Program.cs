string entrance = "2+*3";
Lexer lexer = new Lexer(entrance);
//Console.WriteLine(string.Join('\n', lexer.TokenList));f
Parser parser = new Parser(lexer.TokenList);
Interpreter interpreter = new Interpreter(parser);
Console.WriteLine(Convert.ToString(interpreter.Interpreter0()));
