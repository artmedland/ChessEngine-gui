public class Knight : Piece
{
    public override char UnicodeSymbol => '♞';
    public override char AsciiSymbol => Color == PieceColor.White ? 'N' : 'n';
    public override int Value => 3;
}