using System.Text;
class Program
{
    public static bool UseUnicodeSymbols { get; set; } = true;
    
    const string HelpText = @"
Available commands:

    draw <FEN>
        Draws the chessboard using the given FEN string

    play
        Starts a new game. You may be prompted for player side and board state

    use ascii
        Displays pieces using ASCII symbols: K Q R B N P

    use unicode
        Displays pieces using Unicode symbols: ♚ ♛ ♜ ♝ ♞ ♟

    clear
        Clears the console screen

    help
        Displays this help message

    exit
        Exits the program
    ";

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        //PromptSymbolStyle();
        Console.Clear();
        Console.WriteLine("Type help for a list of commands");

        while (true)
        {
            if(!HandleInput())
                break;        
        }
    }
    
    static bool HandleInput()
    {
        string? input = Console.ReadLine()?.Trim();
        
        if (input == null)
            return true;
            
        string inputLower = input.ToLower();
        
        if (inputLower.StartsWith("draw"))
        {
            DrawBoard(input);
        }   
        else if (inputLower.StartsWith("play"))
        {
            Game(); //play playerSide FEN
        }
        else if(inputLower == "clear")
        {
            Console.Clear();
        }
        else if(inputLower == "use ascii")
        {
            UseUnicodeSymbols = false;
            Console.WriteLine();
            Console.WriteLine("Using ASCII");
            Console.WriteLine("Pieces: K Q R B N P");
            Console.WriteLine();
        }
        else if(inputLower == "use unicode")
        {
            UseUnicodeSymbols = true;
            Console.WriteLine();
            Console.WriteLine("Using Unicode");
            Console.WriteLine("Pieces: ♚ ♛ ♜ ♝ ♞ ♟");
            Console.WriteLine();
        }
        else if(inputLower == "help" || inputLower == "h")
        {
            Console.WriteLine(HelpText);
        }
        else if(inputLower == "exit")
        {
            return false;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Unknown command");
            Console.WriteLine("Type help for a list of commands");
            Console.WriteLine();
        }

        return true;
    }
    
    static void Game()
    {
        Board board = new();
        while(true)
        {
            Console.WriteLine();
            Console.WriteLine(board.GenerateFEN());
            Console.WriteLine();
            board.Draw();
            Console.WriteLine();
            
            while(true)
            {
                Console.Write($"Enter move ({board.CurrentTurn}): ");
                string? input = Console.ReadLine()?.Trim().ToLower();
                
                if(input == null)
                {
                    Console.WriteLine("No input");
                    continue;
                }
                if(input == "stop" || input == "end")
                {
                    Console.WriteLine();
                    Console.WriteLine("Game ended");
                    Console.WriteLine();
                    return;
                }

                Move move;
                
                try
                {
                    Coordinate fromCoordinate;
                    Coordinate toCoordinate;
                
                    if(input.Length == 4)
                    {
                        fromCoordinate = new(input.Substring(0, 2));
                        toCoordinate = new(input.Substring(2, 2));                    
                    }
                    else
                    {
                        string[] coordinates = input.Split(' ');
                        fromCoordinate = new(coordinates[0]);
                        toCoordinate = new(coordinates[1]);    
                    }

                    move = new(fromCoordinate, toCoordinate);
                }
                catch(Exception)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                
                if(!GameLogic.IsLegalMove(board, move))
                {
                    Console.WriteLine("Illegal move");
                    continue;
                }
                else
                {
                    GameLogic.ApplyMove(board, move);
                    break;
                }        
            }       
        }
    }   

    static void PromptSymbolStyle()
    {
        Console.WriteLine("Do these look like chess pieces?");
        Console.WriteLine("♚ ♛ ♜ ♝ ♞ ♟");
        
        while (true)
        {        
            Console.Write("(y / n): ");
            string? input = Console.ReadLine()?.Trim().ToLower();
            if (input == "y")
            {
                UseUnicodeSymbols = true;
                break;
            }
            else if (input == "n")
            {
                UseUnicodeSymbols = false;
                break;
            }           
        }
    }

    static void DrawBoard(string input)
    {
        try
        {
            string FEN = input["draw ".Length..].Trim();
            Board board = new(FEN);
            Console.WriteLine();
            board.Draw();
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}



