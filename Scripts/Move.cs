public struct Move
{
    public Coordinate From { get; }
    public Coordinate To { get; }
    
    public Move(Coordinate from, Coordinate to)
    {
        From = from;
        To = to;      
    }
    
    public override string ToString() => $"{From} -> {To}";
}