using DM.Game;

namespace TalkBack.ContactsControl.Data.Services
{
    public interface IGameService
    {
        //Board CreateBoard();
        Board GetBoard();
        Board UpdateBoard(int row, int col);
    }
}
