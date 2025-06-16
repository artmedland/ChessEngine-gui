public class King : Piece
{
    public override char UnicodeSymbol => '♚';
    public override char AsciiSymbol => Color == PieceColor.White ? 'K' : 'k';
    public override int Value => 0;
    
    public override IEnumerable<Move> GetLegalMoves(Board board, Coordinate from)
    {
        return default!;
    }
}