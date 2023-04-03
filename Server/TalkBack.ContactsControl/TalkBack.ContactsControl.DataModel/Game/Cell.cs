namespace DM.Game
{
    public enum CellEnum
    {
        empty = ' ',
        player1 = 'X',
        player2 = 'O'
    }
    public class Cell
    {
        public int Id { get; set; }
        public CellEnum Value { get; set; }
    }
}
