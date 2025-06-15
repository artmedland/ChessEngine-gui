public class Board
{
    public Piece?[,] pieces = new Piece?[8, 8];
    public PieceColor CurrentTurn { get; set; } = PieceColor.White;
    Coordinate? EnPassantCoordinate { get; set; } = null;
    int HalfmoveClock { get; set; } = 0;
    int FullmoveCounter { get; set; } = 1;
    public const string DefaultFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    public Board(string FEN = DefaultFEN)
    {
        SetUpFromFEN(FEN);
    }
    
    void SetUpFromFEN(string FEN)
    {
        string[] splitFEN = FEN.Split(' ');
        
        CurrentTurn = splitFEN[1] == "w" ? PieceColor.White : PieceColor.Black;
        
        string FENpieces = splitFEN[0];        
        string[] rows = FENpieces.Split('/');     
        //So that white is at row 0
        int y = 7;
        
        for(int i = 0; i < rows.Length; i++)
        {
            int x = 0;
  
            for(int j = 0; j < rows[i].Length; j++)
            {
                if(int.TryParse(rows[i][j].ToString(), out int value))
                {
                    x += value;                   
                }
                else
                {
                    var piece = Piece.GetPieceFromSymbol(rows[i][j]);
                    piece.Color = char.IsUpper(rows[i][j]) ? PieceColor.White : PieceColor.Black;
                    //todo: add logic that checks if a piece has been moved, only needed for pawns, kings and rooks
                    piece.HasMoved = false;
                    pieces[x, y] = piece;
                    
                    x++;
                }
            }
            
            y--;
        }
    }
       
    public string GenerateFEN()
    {
        string FEN = "";
            
        for (int row = 7; row >= 0; row--)
        {
            int emptyCount = 0;
            
            for (int col = 0; col < 8; col++)
            {
                Piece? piece = pieces[col, row];
                      
                if(piece == null)
                {
                    emptyCount++;
                    
                    if(col == 7)
                        FEN += emptyCount.ToString();
                                       
                }              
                else
                {
                    if(emptyCount > 0)
                    {
                        FEN += emptyCount.ToString();
                        emptyCount = 0;
                    }
                        
                    FEN += piece.AsciiSymbol;
                }                 
            }
            if(row > 0)
                FEN += "/";
        }

        FEN += " ";
        FEN += CurrentTurn == PieceColor.White ? "w" : "b";
        
        return FEN;
    }
    
    public void Draw()
    {
        for (int row = 7; row >= 0; row--)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(row + 1);
            
            for (int col = 0; col < 8; col++)
            {
                Console.Write(' ');
                Piece? piece = pieces[col, row];
                      
                if(piece == null)
                {
                    Console.ForegroundColor = (row + col) % 2 == 0 ? ConsoleColor.DarkGreen : ConsoleColor.Gray;
                    Console.Write('·');
                    continue;
                }      
                
                if(piece.Color == PieceColor.Black)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }          
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                               
                Console.Write(Program.UseUnicodeSymbols ? piece.UnicodeSymbol : piece.AsciiSymbol);
            }
            Console.WriteLine();
        }
        Console.Write("  "); 
        
        var letters = Enumerable.Range('a', 26).Select(i => (char)i).ToList();
        Console.ForegroundColor = ConsoleColor.Green;
        for (int x = 0; x < 8; x++)
        {
            Console.Write(letters[x]);
            Console.Write(' ');
        }
        Console.ResetColor();
        Console.WriteLine();      
    }
}