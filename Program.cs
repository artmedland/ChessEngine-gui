Engine engine = new();

Console.WriteLine("Welcome to BurkFish, type help for a list of commands");

while(true)
{
    string? input = Console.ReadLine();
    if (input == null)
    {
        return;
    }

    input = input.Trim();
    
    if(input.StartsWith("draw"))
    {      
        try
        {
            string FEN = input["draw ".Length..].Trim();
            Board board = new Board(FEN);
            Console.WriteLine();
            board.Draw();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}