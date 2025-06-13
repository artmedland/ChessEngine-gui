public static class GameLogic
{
    public static void ApplyMove(Board board, Move move)
    {
        if (!IsLegalMove(board, move))
            return;
        
        board.pieces[move.To.Col, move.To.Row] = board.pieces[move.From.Col, move.From.Row];
        board.pieces[move.From.Col, move.From.Row] = null;
    }
    
    public static bool IsLegalMove(Board board, Move move)
    {
        if(board.pieces[move.From.Col, move.From.Row] == null)
            return false;
        

        Piece piece = board.pieces[move.From.Col, move.From.Row]!;

        return piece.Color == board.currentTurn;
    }
    
    public static void IsLegalPosition(Board board)
    {
        
    }
}