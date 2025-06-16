public class Pawn : Piece
{
    public override char UnicodeSymbol => '♟';
    public override char AsciiSymbol => Color == PieceColor.White ? 'P' : 'p';
    public override int Value => 1;
    
    public override IEnumerable<Move> GetLegalMoves(Board board, Coordinate from)
    {
        return default!;
    }
}