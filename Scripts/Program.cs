using System.Text;
class Program
{
    public static bool UseUnicodeSymbols { get; set; } = true;
    
    const string HelpText = @"
Available commands:

    draw <FEN>
        Draws the chessboard using the given FEN string

    play <MODE> <FEN>
        Modes: w, b, p, e
        Move example: e2 e4
        Commands: undo, get moves, end
        Starts a new game

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
        string input = Console.ReadLine()?.Trim() ?? "";
            
        string inputLower = input.ToLower();
        
        if (inputLower.StartsWith("draw"))
        {
            DrawBoard(input);
        }   
        else if (inputLower.StartsWith("play"))
        {
            string[] inputSplit = input.Split(' ');
            Console.WriteLine(inputSplit.Length);
                  
            if(inputSplit.Length == 1)
            {
                StartGame();
            }               
            else if(inputSplit.Length == 2)
            {
                char mode = inputSplit[1].ToLower()[0];
                Console.WriteLine(mode);
                StartGame(mode);
            }            
            else
            {
                char mode = inputSplit[1][0];
                string FEN = string.Join(" ", inputSplit.Skip(2));
                StartGame(mode, FEN);
            }
                
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
        else if(input == "")
        {
            return true;
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
    
    static void StartGame(char modeChar = 'w', string FEN = Board.DefaultFEN)
    {
        Board board;
        Mode mode;
        PieceColor playerSide = PieceColor.White;
        
        try
        {
            mode = GetMode(modeChar);
            board = new(FEN);
        }
        catch(Exception)
        {
            Console.WriteLine();
            Console.WriteLine("Invalid input");
            Console.WriteLine();
            return;
        }
        
        if(mode == Mode.pVe)
            playerSide = modeChar == 'w' ? PieceColor.White : PieceColor.Black; 
        
        
        switch(mode)
        {
            case Mode.pVp:
                PlayerVPlayerGame(board);
                break;
            case Mode.pVe:
                PlayerVEngineGame(board, playerSide);
                break;
        }
    }
    
    static void PlayerVPlayerGame(Board board)
    {
        Stack<string> FENhistory = new();   
        while(true) //Game loop
        {        
            string currentFEN = board.GenerateFEN();
            
            Console.WriteLine();          
            Console.WriteLine(currentFEN);        
            
            Console.WriteLine();
            board.Draw();
            Console.WriteLine();
            
            if(GameLogic.IsCheckmate(board))
            {
                Console.WriteLine("Checkmate");
                return;
            }
            if(GameLogic.IsStalemate(board))
            {
                Console.WriteLine("Stalemate");
                return;
            }      
            
            if(!ApplyPlayerMove(board, currentFEN, FENhistory))
                return;      
        }
    }
    
    static void PlayerVEngineGame(Board board, PieceColor playerSide)
    {
        Stack<string> FENhistory = new();
        Engine engine = new();

        while(true) //Game loop
        {             
            string currentFEN = board.GenerateFEN();
            
            Console.WriteLine();          
            Console.WriteLine(currentFEN);  
            Console.WriteLine();
            board.Draw();
            Console.WriteLine();
            
            if(GameLogic.IsCheckmate(board))
            {
                Console.WriteLine("Checkmate");
                Console.WriteLine();
                return;
            }
            if(GameLogic.IsStalemate(board))
            {
                Console.WriteLine("Stalemate");
                Console.WriteLine();
                return;
            }              
        
            if(board.CurrentTurn == playerSide)
            {
                if(!ApplyPlayerMove(board, currentFEN, FENhistory))
                    break; 
            }
            else
            {
                engine.MakeMove(board);
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

    static bool ApplyPlayerMove(Board board, string currentFEN, Stack<string> fenHistory)
    {
        while(true) //Input loop
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
                return false;
            }
            if(input == "undo" || input == "u")
            {
                if(fenHistory.Count == 0)
                {
                    Console.WriteLine("No moves to undo");
                    continue;
                }

                board.SetUpFromFEN(fenHistory.Pop());
                break;
            }
            if(input == "get moves")
            {
                Console.WriteLine();
                foreach(Move m in GameLogic.GetAllLegalMoves(board))
                    Console.WriteLine(m);
                Console.WriteLine();
                continue;
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
                fenHistory.Push(currentFEN);
                
                if(board.pieces[move.From.Col, move.From.Row] is Pawn && (move.To.Row == 0 || move.To.Row == 7)) //If promotion
                {
                    while(true)
                    {
                        Console.Write("Promote pawn to (q / r / b / n): ");
                        string? promotionPieceInput = Console.ReadLine()?.Trim().ToLower();
                        
                        if(promotionPieceInput == null)
                        {
                            Console.WriteLine("No input");
                            continue;
                        }
                        if (promotionPieceInput == "q" || promotionPieceInput == "r" || promotionPieceInput == "b" || promotionPieceInput == "n")
                        {
                            GameLogic.ApplyMove(board, move, promotionPieceInput[0]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                            continue;
                        }
                        
                    }
                }
                else
                {
                    GameLogic.ApplyMove(board, move);
                }
                
                break;                        
            }      
        }
        
        return true;     
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
    
    static Mode GetMode(char c)
    {
        return c.ToString().ToLower() switch
        {
            "w" => Mode.pVe,
            "b" => Mode.pVe,
            "e" => Mode.eVe,
            "p" => Mode.pVp,
            _ => throw new Exception("Invalid mode")
        };
    }
    
    enum Mode
    {
        pVp,
        pVe,
        eVe,
    }
}