namespace DM.Game
{
    public class Board
    {
        public Cell[][] board = {
            new Cell[]
            {
                new Cell()  {Id = 1, Value = CellEnum.empty},
                new Cell()  {Id = 2, Value = CellEnum.empty},
                new Cell()  {Id = 3, Value = CellEnum.empty},
            },
            new Cell[]
            {
                new Cell()  {Id = 4, Value = CellEnum.empty},
                new Cell()  {Id = 5, Value = CellEnum.empty},
                new Cell()  {Id = 6, Value = CellEnum.empty},
            },
            new Cell[]
            {
                new Cell()  {Id = 7, Value = CellEnum.empty},
                new Cell()  {Id = 8, Value = CellEnum.empty},
                new Cell()  {Id = 9, Value = CellEnum.empty},
            },
        };
        public CellEnum winner { get; set; }
    }
}
        




