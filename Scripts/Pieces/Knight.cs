public class Knight : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'N' : 'n';
}