public class Queen : Piece
{
    public override char UnicodeSymbol => '♛';
    public override char AsciiSymbol => Color == PieceColor.White ? 'Q' : 'q';
    public override int Value => 9;
    
    static readonly Coordinate[] directions =
    [
        new Coordinate(1, 1),
        new Coordinate(1, -1),
        new Coordinate(-1, 1),
        new Coordinate(-1, -1),
        new Coordinate(0, 1),
        new Coordinate(0, -1),
        new Coordinate(1, 0),
        new Coordinate(-1, 0),
    ];
    
    public override IEnumerable<Move> GetPseudoLegalMoves(Board board, Coordinate from)
    {
        foreach(var move in GameLogic.GetSlidingMoves(board, from, directions, 7))
        {
            yield return move;
        }
    }
}