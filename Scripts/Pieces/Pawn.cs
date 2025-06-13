public class Pawn : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'P' : 'p';
}