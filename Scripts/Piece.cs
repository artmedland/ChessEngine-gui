using System.Runtime;

public abstract class Piece
{
    public PieceColor Color { get; set; }
    public bool HasMoved { get; set; } = false; //Only relevant for pawns, kings and rooks
    public abstract char UnicodeSymbol { get; }
    public abstract char AsciiSymbol { get; }
    public abstract int Value { get; }
    
    //Maybe return "to" coordinate instead, depends
    //public abstract IEnumerable<Move> GetLegalMoves(Board board, Coordinate from); 
    
    public static Piece GetPieceFromSymbol(char symbol)
    {
        return symbol.ToString().ToLower() switch
        {
            "p" => new Pawn(),
            "n" => new Knight(),
            "b" => new Bishop(),
            "r" => new Rook(),
            "q" => new Queen(),
            "k" => new King(),
            _ => throw new Exception($"Invalid piece symbol: {symbol}")
        };
    }
          
}
public enum PieceColor
{
    White,
    Black
}