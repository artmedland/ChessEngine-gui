public class Queen : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'Q' : 'q';
}