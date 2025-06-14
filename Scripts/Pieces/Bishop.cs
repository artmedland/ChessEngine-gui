public class Bishop : Piece
{
    public override char UnicodeSymbol => '♝';
    public override char AsciiSymbol => Color == PieceColor.White ? 'B' : 'b';
}