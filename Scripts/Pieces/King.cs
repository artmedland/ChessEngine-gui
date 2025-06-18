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
        new Coordinate(1, 1),
        new Coordinate(1, -1),
        new Coordinate(-1, 1),
        new Coordinate(-1, -1),
    ];
    
    public override IEnumerable<Move> GetPseudoLegalMoves(Board board, Coordinate from)
    {
        foreach(var move in GameLogic.GetSlidingMoves(board, from, directions, 1))
        {
            yield return move;
        }
        
        //castling
        if(!HasMoved && !GameLogic.IsCheck(board, board.CurrentTurn))
        {
            PieceColor opposingSide = OppositeColor(board.CurrentTurn);
            
            //kingside
             if(board.pieces[7,from.Row] is Rook rookKingside && !rookKingside.HasMoved)
             {
                 if(board.pieces[5, from.Row] == null && board.pieces[6, from.Row] == null)
                 {
                    if(!GameLogic.IsSquareAttacked(board, new(5, from.Row), opposingSide) && 
                       !GameLogic.IsSquareAttacked(board, new(6, from.Row), opposingSide))
                    {
                        yield return new(from, new(6, from.Row));
                    }
                    
                 }
             }
             
             //queenside
             if(board.pieces[0,from.Row] is Rook rookQueenside && !rookQueenside.HasMoved)
             {
                 if(board.pieces[1, from.Row] == null && board.pieces[2, from.Row] == null  && board.pieces[3, from.Row] == null)
                 {
                     if(!GameLogic.IsSquareAttacked(board, new(1, from.Row), opposingSide) && 
                        !GameLogic.IsSquareAttacked(board, new(2, from.Row), opposingSide) &&
                        !GameLogic.IsSquareAttacked(board, new(3, from.Row), opposingSide))
                    {
                        yield return new(from, new(2, from.Row));
                    }                                 
                 }
             }
        }                             
    }
    
    public override IEnumerable<Move> GetAttackingSquares(Board board, Coordinate from)
    {
        foreach(var move in GameLogic.GetSlidingMoves(board, from, directions, 1))
        {
            yield return move;
        }                       
    }
    
    public override Piece Clone()
    {
        return new King
        {
            Color = this.Color,
            HasMoved = this.HasMoved
        };
    }
}