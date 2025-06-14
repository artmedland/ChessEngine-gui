public static class GameLogic
{
    public static void ApplyMove(Board board, Move move) //ALWAYS CHECK IF MOVE IS LEGAL BEFORE APPLYING MOVE
    {
        Piece pieceToMove = board.pieces[move.From.Col, move.From.Row]!;
        board.pieces[move.To.Col, move.To.Row] = pieceToMove;
        pieceToMove.HasMoved = true;
        board.pieces[move.From.Col, move.From.Row] = null;
        board.CurrentTurn = board.CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
    
    public static bool IsLegalMove(Board board, Move move)
    {
        if (move.From == move.To)
            return false;
        
        if(!(IsOnBoard(move.From) && IsOnBoard(move.To)))
            return false;
    
        if(board.pieces[move.From.Col, move.From.Row] == null)
            return false;     

        Piece piece = board.pieces[move.From.Col, move.From.Row]!;

        return piece.Color == board.CurrentTurn;
    }
    
    public static bool IsLegalPosition(Board board)
    {
        return true;
    }
    
    public static bool IsCheck(Board board, PieceColor color)
    {
        return true;
    }
    
    public static bool IsCheckmate(Board board, PieceColor color)
    {
        return true;
    }
    
    public static bool IsOnBoard(Coordinate coord)
    {
        return coord.Col is >= 0 and <= 7 && coord.Row is >= 0 and <= 7;
    }
}