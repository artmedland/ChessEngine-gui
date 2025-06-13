public struct Coordinate
{
    public int Row { get; }
    public int Col { get; }
    
    public Coordinate(int col, int row)
    {
        Col = col;
        Row = row;      
    }
    
    public Coordinate(string notation)
    {
        if (notation.Length != 2)
            throw new Exception("Invalid coordinate: " + notation);

        Col = notation[0] - 'a';
        Row = notation[1] - '1'; 
    }
    
    public override string ToString() => $"{(char)('a' + Col)}{Row + 1}";
}