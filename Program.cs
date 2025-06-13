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
            Console.WriteLine();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
    //play playerSide FEN
    else if(input.StartsWith("play"))
    {
        Game();
    }
}

void Game()
{
    Board board = new();
    while(true)
    {
        Console.WriteLine();
        board.Draw();
        Console.WriteLine();
        
        while(true)
        {
            Console.Write($"Enter move ({board.currentTurn}): ");
            string? input = Console.ReadLine();
            
            if(input == null)
            {
                Console.WriteLine("No input");
                continue;
            }
            if(input == "stop")
            {
                Console.WriteLine("Game stopped");
                return;
            }

            Move move;
            
            try
            {
                Coordinate fromCoordinate;
                Coordinate toCoordinate;
            
                if(input.Length == 4)
                {
                    fromCoordinate = new(input.Substring(0, 2));
                    toCoordinate = new(input.Substring(2, 2));                    
                }
                else
                {
                    string[] coordinates = input.Split(' ');
                    fromCoordinate = new(coordinates[0]);
                    toCoordinate = new(coordinates[1]);    
                }

                move = new(fromCoordinate, toCoordinate);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input");
                continue;
            }
            
            if(!GameLogic.IsLegalMove(board, move))
            {
                Console.WriteLine("Illegal move");
                continue;
            }
            else
            {
                GameLogic.ApplyMove(board, move);
                break;
            }        
        }       
    }
}