using System.Collections.Generic;

namespace GameOfLife
{
    public class Cell
    {
        // TODO: Delete ROW and Column, not needed
        public Row Row { get; }
        public Column Column { get; }
        public int RowLocation { get; }
        public int ColumnLocation { get; }
        public bool IsLiving { get; set; }
        public string NextEvolution { get; set; }
        
        public Cell(Row row, Column column, bool isLiving = false, string nextEvolution = "dead")
        {
            Row = row;
            RowLocation = row.Location;
            Column = column;
            ColumnLocation = column.Location;
            IsLiving = isLiving;
            NextEvolution = nextEvolution;
        }

        // OVERLOAD: This is made for seeds
        public Cell(int rowLocation, int columnLocation)
        {
            RowLocation = rowLocation;
            ColumnLocation = columnLocation;
        }

    }
}