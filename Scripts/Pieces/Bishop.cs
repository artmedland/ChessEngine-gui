public class Bishop : Piece
{
    public override char UnicodeSymbol => '♝';
    public override char AsciiSymbol => Color == PieceColor.White ? 'B' : 'b';
    public override int Value => 3;
    
    static readonly Coordinate[] directions =
    [
        new Coordinate(1, 1),
        new Coordinate(1, -1),
        new Coordinate(-1, 1),
        new Coordinate(-1, -1),
    ];
    
    public override IEnumerable<Move> GetPseudoLegalMoves(Board board, Coordinate from)
    {
        foreach(var move in GameLogic.GetSlidingMoves(board, from, directions, 7))
        {
            yield return move;
        }
    }
}