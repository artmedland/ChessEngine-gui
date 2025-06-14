public class Queen : Piece
{
    public override char UnicodeSymbol => '♛';
    public override char AsciiSymbol => Color == PieceColor.White ? 'Q' : 'q';
    public override int Value => 9;
}