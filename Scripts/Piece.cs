public class Piece
{
    public PieceColor Color { get; set; }
    public PieceType Type { get; set; }
    public bool hasMoved { get; set; }
    public char Symbol 
    {
        get
        {
            switch(Type)
            {
                case PieceType.Pawn:
                    return Color == PieceColor.White ? 'P' : 'p';
                case PieceType.Knight:
                    return Color == PieceColor.White ? 'N' : 'n';
                case PieceType.Bishop:
                    return Color == PieceColor.White ? 'B' : 'b';
                case PieceType.Rook:
                    return Color == PieceColor.White ? 'R' : 'r';
                case PieceType.Queen:
                    return Color == PieceColor.White ? 'Q' : 'q';
                case PieceType.King:
                    return Color == PieceColor.White ? 'K' : 'k';
                default:
                    throw new InvalidOperationException($"Unknown piece type: {Type}");
            }
        }
    } 

    public static PieceType GetPieceFromSymbol(char symbol)
    {
        switch(symbol.ToString().ToLower())
        {
            case "p":
                return PieceType.Pawn;
            case "n":
                return PieceType.Knight;
            case "b":
                return PieceType.Bishop;
            case "r":
                return PieceType.Rook;
            case "q":
                return PieceType.Queen;
            case "k":
                return PieceType.King;
            default:
                throw new ArgumentException($"Invalid piece symbol: {symbol}");
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