public class Board
{
    public Piece?[,] pieces = new Piece?[8, 8];
    public PieceColor CurrentTurn { get; set; } = PieceColor.White;
    public Coordinate? EnPassantCoordinate { get; set; } = null;
    public int HalfmoveClock { get; set; } = 0;
    public int FullmoveCounter { get; set; } = 1;
    public const string DefaultFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    public Board(string FEN = DefaultFEN)
    {
        SetUpFromFEN(FEN);
    }
    
    public void SetUpFromFEN(string FEN)
    {
        string[] splitFEN = FEN.Split(' ');
        
        CurrentTurn = splitFEN[1] == "w" ? PieceColor.White : PieceColor.Black;
        HalfmoveClock = int.Parse(splitFEN[4]);
        FullmoveCounter = int.Parse(splitFEN[5]);
        EnPassantCoordinate = splitFEN[3] == "-" ? null : new Coordinate(splitFEN[3]);
        
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

        string castleString = splitFEN[2];
        if(castleString.Contains('K') || castleString.Contains('Q'))
        {
            if(pieces[4,0] is not King whiteKing)
            {
                throw new Exception("Invalid FEN");
            }
            else
            {
                whiteKing.HasMoved = false;
            }
            
            if(castleString.Contains('K'))
            {
                if(pieces[7,0] is not Rook rookKingside)
                {
                    throw new Exception("Invalid FEN");
                }
                else
                {
                    rookKingside.HasMoved = false;
                }
            }
            if(castleString.Contains('Q'))
            {
                if(pieces[7,0] is not Rook rookQueenSide)
                {
                    throw new Exception("Invalid FEN");
                }
                else
                {
                    rookQueenSide.HasMoved = false;
                }
            }
        }
        if(castleString.Contains('k') || castleString.Contains('q'))
        {
            if(pieces[4,7] is not King blackKing)
            {
                throw new Exception("Invalid FEN");
            }
            else
            {
                blackKing.HasMoved = false;
            }
            
            if(castleString.Contains('k'))
            {
                if(pieces[7,7] is not Rook rookKingside)
                {
                    throw new Exception("Invalid FEN");
                }
                else
                {
                    rookKingside.HasMoved = false;
                }
            }
            if(castleString.Contains('q'))
            {
                if(pieces[0,7] is not Rook rookQueenSide)
                {
                    throw new Exception("Invalid FEN");
                }
                else
                {
                    rookQueenSide.HasMoved = false;
                }
            }
        }
    }
       
    public string GenerateFEN()
    {
        string FENpieces = "";
            
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
                        FENpieces += emptyCount.ToString();
                                       
                }              
                else
                {
                    if(emptyCount > 0)
                    {
                        FENpieces += emptyCount.ToString();
                        emptyCount = 0;
                    }
                        
                    FENpieces += piece.AsciiSymbol;
                }                 
            }
            if(row > 0)
                FENpieces += "/";
        }

        string FENturn = CurrentTurn == PieceColor.White ? "w" : "b";

        string castleString = "";
        
        if(pieces[4,0] is King whiteKing && !whiteKing.HasMoved)
        {
            if(pieces[7,0] is Rook rookKingside  && !rookKingside.HasMoved)
            {
                castleString += "K";
            }
            if(pieces[0,0] is Rook rookQueenSide && !rookQueenSide.HasMoved)
            {
                castleString += "Q";
            }
        }
        if(pieces[4,7] is King blackKing && !blackKing.HasMoved)
        {
            if(pieces[7,7] is Rook rookKingside  && !rookKingside.HasMoved)
            {
                castleString += "k";
            }
            if(pieces[0,7] is Rook rookQueenSide && !rookQueenSide.HasMoved)
            {
                castleString += "q";
            }
        }
        castleString = castleString == "" ? "-" : castleString;

        string FENenPassantCoordinate = EnPassantCoordinate?.ToString() ?? "-";
 
        return $"{FENpieces} {FENturn} {castleString} {FENenPassantCoordinate} {HalfmoveClock} {FullmoveCounter}";
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