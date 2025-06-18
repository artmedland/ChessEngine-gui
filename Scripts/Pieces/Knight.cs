public class Knight : Piece
{
    public override char UnicodeSymbol => '♞';
    public override char AsciiSymbol => Color == PieceColor.White ? 'N' : 'n';
    public override int Value => 3;
    static readonly Coordinate[] KnightMoves =
    [
        new Coordinate(1, 2),
        new Coordinate(2, 1),
        new Coordinate(-1, 2),
        new Coordinate(-2, 1),
        new Coordinate(1, -2),
        new Coordinate(2, -1),
        new Coordinate(-1, -2),
        new Coordinate(-2, -1)
    ];
    
    public override IEnumerable<Move> GetPseudoLegalMoves(Board board, Coordinate from)
    {
        foreach(Coordinate offset in KnightMoves)
        {
            Coordinate to = from + offset;
            
            if(!GameLogic.IsOnBoard(to))
                continue;
            
            if(board.pieces[to.Col, to.Row] is Piece piece && piece.Color == board.CurrentTurn)
                continue;
                        
            yield return new Move(from, to);               
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
        return new Knight
        {
            Color = this.Color,
        };
    }
}