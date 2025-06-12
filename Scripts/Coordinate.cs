public struct Coordinate
{
    public int Row { get; }
    public int Col { get; }
    
    public Coordinate(int col, int row)
    {
        Col = col;
        Row = row;      
    }
    
    public override string ToString() => $"{(char)('a' + Col)}{Row + 1}";
}