public class Bishop : Piece
{
    public override char UnicodeSymbol => '♝';
    public override char AsciiSymbol => Color == PieceColor.White ? 'B' : 'b';
    public override int Value => 3;
    
    public override IEnumerable<Move> GetLegalMoves(Board board, Coordinate from)
    {
        return default!;
    }
}