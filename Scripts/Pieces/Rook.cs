public class Rook : Piece
{
    public override char UnicodeSymbol => '♜';
    public override char AsciiSymbol => Color == PieceColor.White ? 'R' : 'r';
    public override int Value => 5;
}