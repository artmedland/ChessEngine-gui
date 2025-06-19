public class Engine
{

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

            float score = AlphaBeta(tempBoard, depth - 1, float.MinValue, float.MaxValue, Piece.OppositeColor(board.CurrentTurn), board.CurrentTurn);
            
            if(score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }
        
        return bestMove ?? throw new Exception("No legal moves found");
    }
    
    float AlphaBeta(Board board, int depth, float alpha, float beta, PieceColor side, PieceColor originalSide)
    {
        if(depth == 0 || GameLogic.IsGameOver(board))
            return Evaluate(board, originalSide);
        
        if(side == PieceColor.White)
        {
            float maxEval = float.MinValue;
            
            foreach(Move move in GameLogic.GetAllLegalMoves(board))
            {
                Board tempboard = board.Clone();
                GameLogic.ApplyMove(tempboard, move);
                float eval = AlphaBeta(tempboard, depth - 1, alpha, beta, PieceColor.Black, originalSide);
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
                float eval = AlphaBeta(tempboard, depth - 1, alpha, beta, PieceColor.White, originalSide);
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
        
        finalScore += GameLogic.GetMaterialBalance(board);
        finalScore += GetAttackedSquaresScore(board, PieceColor.White);
        finalScore -= GetAttackedSquaresScore(board, PieceColor.Black);
        
        return side == PieceColor.White ? finalScore : -finalScore; 
    }
    
    float GetAttackedSquaresScore(Board board, PieceColor side)
    {
        float score = 0;
        
        foreach(Coordinate square in GameLogic.GetAllAttackedSquares(board, side))
        {
            if(side == PieceColor.White)
            {
                score += 0.01f;
                
                if(square.Row > 3 && square.Row < 6 && square.Col > 1 && square.Col < 6)
                {
                    score += 0.01f;
                    
                    if(square.Row == 4 && (square.Col == 3 || square.Col == 4))
                        score += 0.02f;
                }
            }
            else
            {
                score += 0.01f;

                if(square.Row > 1 && square.Row < 4 && square.Col > 1 && square.Col < 6)
                {
                    score += 0.01f;
                    
                    if(square.Row == 3 && (square.Col == 3 || square.Col == 4))
                        score += 0.02f;
                }        
            }
        }
        return Math.Clamp(score, 0, 0.5f);
    }

    float ScoreMove(Board board, Move move)
    {
        Board tempBoard = board.Clone();
        GameLogic.ApplyMove(tempBoard, move);
        return Evaluate(tempBoard, board.CurrentTurn);
    }
}