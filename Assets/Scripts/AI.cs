using UnityEngine;

public class AI
{
    public static Vector2Int GetBestMove(CellState[,] board) {
        int bestScore = int.MinValue;
        Vector2Int bestMove = new Vector2Int(-1, -1);

        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                if (board[row, col] == CellState.None) {
                    board[row, col] = CellState.O;  // AI tries this move
                    int score = Minimax(board, 0, false);
                    board[row, col] = CellState.None;

                    if (score > bestScore) {
                        bestScore = score;
                        bestMove = new Vector2Int(row, col);
                    }
                }
            }
        }

        return bestMove;
    }
    
    static int Minimax(CellState[,] board, int depth, bool isMaximizing) {
        CellState winner = CheckWinner(board);
        if (winner == CellState.O) return 10 - depth;    // Enemy wins
        if (winner == CellState.X) return depth - 10;    // Player wins
        if (IsBoardFull(board)) return 0;                // Draw

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == CellState.None)
                {
                    board[row, col] = isMaximizing ? CellState.O : CellState.X;
                    int score = Minimax(board, depth + 1, !isMaximizing);
                    board[row, col] = CellState.None;

                    if (isMaximizing)
                        bestScore = Mathf.Max(score, bestScore);
                    else
                        bestScore = Mathf.Min(score, bestScore);
                }
            }
        }

        return bestScore;
    }

    public static bool IsBoardFull(CellState[,] board) {
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                if (board[row, col] == CellState.None) {
                    return false;
                }
            }
        }
        
        return true;
    }

    public static CellState CheckWinner(CellState[,] board) {
        // Rows and columns
        for (int i = 0; i < 3; i++) {
            if (board[i, 0] != CellState.None && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) {
                return board[i, 0];
            }

            if (board[0, i] != CellState.None && board[0, i] == board[1, i] && board[1, i] == board[2, i]) {
                return board[0, i];
            }
        }

        // Diagonals
        if (board[0, 0] != CellState.None && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) {
            return board[0, 0];
        }

        if (board[0, 2] != CellState.None && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) {
            return board[0, 2];
        }

        return CellState.None;
    }
}
