public static class GameLogic
{
    public static void ApplyMove(Board board, Move move, char promotionPieceSymbol = 'q') //ALWAYS CHECK IF MOVE IS LEGAL BEFORE USING THIS METHOD
    {                  
        Piece pieceToMove = board.pieces[move.From.Col, move.From.Row]!;
        Piece? pieceToCapture = board.pieces[move.To.Col, move.To.Row];
        int pawnDirection = board.CurrentTurn == PieceColor.White ? 1 : -1; //Direction the current side pawns move
        
        if(board.CurrentTurn == PieceColor.Black)
            board.FullmoveCounter++;
            
        if(pieceToCapture == null && pieceToMove is not Pawn)
            board.HalfmoveClock++;     
        else
            board.HalfmoveClock = 0;
        
        //Check if move is an en passant
        if(move.To == board.EnPassantCoordinate)
        {
            Coordinate pieceToCaptureCoord = new(move.To.Col, move.To.Row - pawnDirection);
            board.pieces[pieceToCaptureCoord.Col, pieceToCaptureCoord.Row] = null;
        }
        
        if(CheckEnPassantOpportunity(pieceToMove, move, board, pawnDirection, out Coordinate target))
            board.EnPassantCoordinate = target;
        else
            board.EnPassantCoordinate = null;
            
        board.pieces[move.To.Col, move.To.Row] = pieceToMove;
        board.pieces[move.From.Col, move.From.Row] = null;
        pieceToMove.HasMoved = true;
        
        //Castling
        if(pieceToMove is King && Math.Abs(move.From.Col - move.To.Col) > 1)
        {
            if(move.To.Col == 6) //Kingside
            {
                board.pieces[5, move.To.Row] = board.pieces[7, move.From.Row] ;
                board.pieces[7, move.From.Row] = null;
            }
            else if(move.To.Col == 2) //Queenside
            {
                board.pieces[3, move.To.Row] = board.pieces[0, move.From.Row] ;
                board.pieces[0, move.From.Row] = null;
            }
        }                
        else if(pieceToMove is Pawn && (move.To.Row == 0 || move.To.Row == 7)) //Promotion
        {
            Piece promotionPiece = Piece.GetPieceFromSymbol(promotionPieceSymbol);
            promotionPiece.Color = board.CurrentTurn;
            board.pieces[move.To.Col, move.To.Row] = promotionPiece;         
        }
        
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

        if (piece.Color != board.CurrentTurn)
            return false;
        
        if (!piece.GetLegalMoves(board, move.From).Contains(move)) //todo use hashset maybe
            return false;

        return true;
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
    
    public static bool IsSquareAttacked(Board board, Coordinate coordinate)
    {
        return true;
    }
    
    static bool CheckEnPassantOpportunity(Piece pieceToMove, Move move, Board board, int pawnDirection, out Coordinate target)
    {
        target = default;
        
        if(pieceToMove is not Pawn)
            return false;
        
        if(Math.Abs(move.From.Row - move.To.Row) != 2)
            return false;
                
        int[] adjacentCols = [move.To.Col - 1, move.To.Col + 1];

        foreach (int col in adjacentCols)
        {
            if (col < 0 || col >= 8)
                continue;

            if (board.pieces[col, move.To.Row] is Pawn adjacentPawn && adjacentPawn.Color != board.CurrentTurn)
            {
                int targetRow = move.To.Row - pawnDirection;
                target = new Coordinate(move.To.Col, targetRow);
                return true;
            }
        }
            
        return false;
    }
    public static bool IsOnBoard(Coordinate coord)
    {
        return coord.Col is >= 0 and <= 7 && coord.Row is >= 0 and <= 7;
    }
}