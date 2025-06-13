Console.WriteLine("Welcome to BurkFish, type help for a list of commands");

Board testBoard = new();
testBoard.Draw();
GameLogic.ApplyMove(testBoard, new Move(new Coordinate("e2"), new Coordinate("e4")));
testBoard.Draw();

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
            Console.WriteLine();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}