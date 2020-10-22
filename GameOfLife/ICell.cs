using System.Collections.Generic;

namespace GameOfLife
{
    public interface ICell
    {
        int RowLocation { get; }
        int ColumnLocation { get; }
        bool isNextEvolutionLiving { get; set; }
        bool IsLiving { get; set; }
        List<ICell> Neighbours { get; set; }
    }
}