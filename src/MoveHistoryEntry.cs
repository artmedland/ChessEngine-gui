public class MoveHistoryEntry
{
    public Move Move { get; }
    public Piece? CapturedPiece { get; }
    public Piece? OriginalPiece { get; }
    public bool OriginalHasMoved { get; }
    public Coordinate? PreviousEnPassant { get; }
    public int PreviousHalfmoveClock { get; }
    public int PreviousFullmoveCounter { get; }
    public PieceColor PreviousTurn { get; }
    
    public MoveHistoryEntry(
        Move move,
        Piece? capturedPiece,
        Piece? originalPiece,
        bool originalHasMoved,
        Coordinate? prevEnPassant,
        int prevHalfmove,
        int prevFullmove,
        PieceColor prevTurn)
    {
        Move = move;
        CapturedPiece = capturedPiece;
        OriginalPiece = originalPiece;
        OriginalHasMoved = originalHasMoved;
        PreviousEnPassant = prevEnPassant;
        PreviousHalfmoveClock = prevHalfmove;
        PreviousFullmoveCounter = prevFullmove;
        PreviousTurn = prevTurn;
    }
}