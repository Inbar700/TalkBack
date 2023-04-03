using DM.Game;

namespace TalkBack.ContactsControl.Data.Services
{
    public class GameService : IGameService
    {
        private Board board =new Board();
        private CellEnum currentPlayer = CellEnum.player1; // initialize to player 1

        public Board GetBoard()
        {
            return board;
        }
        // common approach is to keep track of which player's turn it is using a boolean flag or an enum,
        // and then toggle the flag or enum value after each successful move
        public Board UpdateBoard(int row, int col)
        {
            // check if cell is not empty - invalid move (cell is alread occupied)
            if(board.board[row][col].Value != CellEnum.empty)
            {
                return null;
            }
            // set the cell value based on current player
            board.board[row][col].Value = currentPlayer;

            if(CheckRow(row, currentPlayer) || 
               CheckCol(col, currentPlayer) ||
               (row == col && CheckDiagonal(currentPlayer)) ||
               (row + col == 3-1 && CheckAntiDiagonal(currentPlayer)))
               {
                    board.winner = currentPlayer;
               }

            // toggle the current player to the other player's value,
            // so that the next move will be made by the other player.
            currentPlayer = (currentPlayer == CellEnum.player1) ? CellEnum.player2 : CellEnum.player1;

            return board;
        }
        private bool CheckRow(int row, CellEnum player)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board.board[row][col].Value != player)
                    return false;
            }
            return true;   
        }
        private bool CheckCol(int col, CellEnum player)
        {
            for (int row = 0; row < 3; row++)
            {
                if (board.board[row][col].Value != player)
                    return false;
            }
            return true;
        }
        private bool CheckDiagonal(CellEnum player)
        {
            for(int row=0; row<3; row++)
            {
                if (board.board[row][row].Value != player)
                    return false;
            }
            return true;
        }
        private bool CheckAntiDiagonal(CellEnum player)
        {
            for(int row=0; row<3; row++)
            {
                if (board.board[row][3 - 1 - row].Value != player)
                    return false;
            }
            return true;
        }
    }
}
