public class King : Piece
{
    public override char UnicodeSymbol => '♚';
    public override char AsciiSymbol => Color == PieceColor.White ? 'K' : 'k';
    public override int Value => 0;
    
    static readonly Coordinate[] directions =
    [
        new Coordinate(0, 1),
        new Coordinate(0, -1),
        new Coordinate(1, 0),
        new Coordinate(-1, 0),
    ];
    
    public override IEnumerable<Move> GetPseudoLegalMoves(Board board, Coordinate from)
    {
        foreach(var move in GameLogic.GetSlidingMoves(board, from, directions, 1))
        {
            yield return move;
        }
    }
}