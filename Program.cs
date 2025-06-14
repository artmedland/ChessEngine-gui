using System.Text;
class Program
{
    public static bool UseUnicodeSymbols { get; set; } = true;

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        PromptSymbolStyle();
        Console.Clear();
        Console.WriteLine("Welcome to BurkFish, type help for a list of commands");

        while (true)
        {
            HandleInput();
        }
    }
    
    static void HandleInput()
    {
        string? input = Console.ReadLine()?.Trim();
        
        if (input == null)
            return;
        
        if (input.StartsWith("draw"))
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
        //play playerSide FEN
        else if (input.StartsWith("play"))
        {
            Game();
        }
    }
    
    static void Game()
    {
        Board board = new();
        while(true)
        {
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
                if(input == "stop")
                {
                    Console.WriteLine("Game stopped");
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
        Console.WriteLine("Do these look like chess pieces to you?");
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
}



