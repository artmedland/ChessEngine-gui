public class Engine
{
    public void MakeMove(Board board)
    {
        Random rand = new Random();
        Move[] legalMoves = GameLogic.GetAllLegalMoves(board).ToArray();
        GameLogic.ApplyMove(board, legalMoves[rand.Next(legalMoves.Length)]);
    }
}