using System.Collections.Generic;

namespace GameOfLife
{
    public class Cell : ICell
    {
        public int RowLocation { get; }
        public int ColumnLocation { get; }
        public bool IsLiving { get; set; }
        public bool isNextEvolutionLiving { get; set; }
        public List<ICell> Neighbours { get; set; }
        
        public Cell(int rowLocation, int columnLocation, bool isLiving = false, bool isNextEvolution = false)
        {
            RowLocation = rowLocation;
            ColumnLocation = columnLocation;
            IsLiving = isLiving;
            isNextEvolutionLiving = isNextEvolution;
        }
    }
}