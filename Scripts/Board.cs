public class Board
{
    Piece[,] pieces = new Piece[8, 8];
    PieceColor currentTurn;
    Coordinate enPassantCoordinate;
    public const string DefaultFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    public Board(string FEN = DefaultFEN)
    {
        SetUpFromFEN(FEN);
    }
    
    void SetUpFromFEN(string FEN)
    {
        string[] splitFEN = FEN.Split(' ');
        
        currentTurn = splitFEN[1] == "w" ? PieceColor.White : PieceColor.Black;
        
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
                    pieces[x, y] = new Piece();
                    pieces[x, y].Color = char.IsUpper(rows[i][j]) ? PieceColor.White : PieceColor.Black;
                    pieces[x, y].Type = Piece.GetPieceFromSymbol(rows[i][j]);
                    
                    x++;
                }
            }
            
            y--;
        }
    }
       
    public void Draw()
    {
        for (int row = 7; row >= 0; row--)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(row + 1);
            Console.ResetColor();
            
            for (int col = 0; col < 8; col++)
            {
                Console.Write(' ');  
                      
                if(pieces[col, row] == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write('-');
                    Console.ResetColor();
                    continue;
                }      
                
                if(pieces[col,row].Color == PieceColor.Black)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                
                Console.Write(pieces[col, row].Symbol);
                Console.ResetColor();

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