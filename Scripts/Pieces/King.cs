public class King : Piece
{
    public override char UnicodeSymbol => '♚';
    public override char AsciiSymbol => Color == PieceColor.White ? 'K' : 'k';
}