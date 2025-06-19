public class Bishop : Piece
{
    public override char UnicodeSymbol => '♝';
    public override char AsciiSymbol => Color == PieceColor.White ? 'B' : 'b';
    public override int Value => Color == PieceColor.White ? 3 : -3;
    
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
    
    public override IEnumerable<Move> GetAttackingSquares(Board board, Coordinate from)
    {
        foreach(var move in GetPseudoLegalMoves(board, from))
        {
            yield return move;
        }                       
    }
    
    public override Piece Clone()
    {
        return new Bishop
        {
            Color = this.Color,
        };
    }
}