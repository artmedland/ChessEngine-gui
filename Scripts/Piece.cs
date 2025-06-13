public abstract class Piece
{
    public PieceColor Color { get; set; }
    public PieceType Type { get; set; }
    public bool hasMoved { get; set; }
    public abstract char Symbol{ get; }
    
    public static Piece GetPieceFromSymbol(char symbol)
    {     
        switch(symbol.ToString().ToLower())
        {
            case "p":
                return new Pawn();
            case "n":
                return new Knight();
            case "b":
                return new Bishop();
            case "r":
                return new Rook();
            case "q":
                return new Queen();
            case "k":
                return new King();
            default:
                throw new Exception($"Invalid piece symbol: {symbol}");
        }
    }
          
}
public enum PieceColor
{
    White,
    Black
}
public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}