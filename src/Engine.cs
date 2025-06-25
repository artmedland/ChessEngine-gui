
public class Engine
{
    static readonly Coordinate[] centerSquares =
    {
        new(3, 3),
        new(3, 4),
        new(4, 3),
        new(4, 4)
    };

    public Move GetBestMove(Board board, int depth = 4)
    {
        Move? bestMove = null;
        float bestScore = float.MinValue;
        
        List<Move> legalMoves = GameLogic.GetAllLegalMoves(board).ToList();
        legalMoves.Sort((a, b) => ScoreMove(board, b).CompareTo(ScoreMove(board, a)));
        
        foreach(Move move in legalMoves)
        {
            Board tempBoard = board.Clone();
            GameLogic.ApplyMove(tempBoard, move);

            float score = AlphaBeta(tempBoard, depth - 1, float.MinValue, float.MaxValue, tempBoard.CurrentTurn, board.CurrentTurn);
            Console.WriteLine($"Move {move} scored {score}");
            if(score > bestScore)
            {
                bestScore = score;
                bestMove = move;
                Console.WriteLine($"New best move: {move} with score {score}");
            }
        }
        
        Console.WriteLine($"Chosen move: {bestMove} with score {bestScore}");
        return bestMove ?? throw new Exception("No legal moves found");
    }
    
    float AlphaBeta(Board board, int depth, float alpha, float beta, PieceColor side, PieceColor rootSide)
    {
        if(depth == 0 || GameLogic.IsGameOver(board))
            return Evaluate(board, rootSide);
        
        if(side == rootSide)
        {
            float maxEval = float.MinValue;
            
            foreach(Move move in GameLogic.GetAllLegalMoves(board))
            {
                Board tempboard = board.Clone();
                GameLogic.ApplyMove(tempboard, move);
                float eval = AlphaBeta(tempboard, depth - 1, alpha, beta, tempboard.CurrentTurn, rootSide);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);                 
        
                if(beta <= alpha)
                    break;
                
            }

            return maxEval;
        }
        else
        {
            float minEval = float.MaxValue;
            
            foreach(Move move in GameLogic.GetAllLegalMoves(board))
            {
                Board tempboard = board.Clone();
                GameLogic.ApplyMove(tempboard, move);
                float eval = AlphaBeta(tempboard, depth - 1, alpha, beta, tempboard.CurrentTurn, rootSide);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                
                if(beta <= alpha)
                    break;
                  
            }

            return minEval;
        }
    }
    
    float Evaluate(Board board, PieceColor side)
    {
        if(GameLogic.IsCheckmate(board))
            return board.CurrentTurn == side ? -10000 : 10000;
            
        if(GameLogic.IsStalemate(board))
            return 0;

        float finalScore = 0;
        
        //Coordinate[] squaresAttackedByWhite = GameLogic.GetAllAttackedSquares(board, PieceColor.White).ToArray();
        //Coordinate[] squaresAttackedByBlack = GameLogic.GetAllAttackedSquares(board, PieceColor.Black).ToArray();
        
        finalScore += GameLogic.GetMaterialBalance(board);
        finalScore += GetAttackedSquaresScore(board, PieceColor.White);
        finalScore -= GetAttackedSquaresScore(board, PieceColor.Black);
        finalScore += GetCenterPawnScore(board, PieceColor.White);
        finalScore -= GetCenterPawnScore(board, PieceColor.Black);
        
        return side == PieceColor.White ? finalScore : -finalScore; 
    }
    
    float GetAttackedSquaresScore(Board board, PieceColor side)
    {
        float score = 0;
        
        foreach(Coordinate square in GameLogic.GetAllAttackedSquares(board, side))
        {
            score += 0.001f;
        }
        return score;
    }
    
    float GetCenterPawnScore(Board board, PieceColor side)
    {
        float score = 0;
        
        foreach (var coord in centerSquares)
        {
            if (board.pieces[coord.Col, coord.Row] is Pawn pawn && pawn.Color == side)
                score += 0.02f;
        }
        
        return score;
    }

    float ScoreMove(Board board, Move move)
    {
        Board tempBoard = board.Clone();
        GameLogic.ApplyMove(tempBoard, move);
        float eval = Evaluate(tempBoard, board.CurrentTurn);

        return eval;
    }
}
