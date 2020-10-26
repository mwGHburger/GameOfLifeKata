using System.Collections.Generic;

namespace GameOfLife
{
    public class Cells : ICells
    {
        public List<ICell> Population { get; } = new List<ICell>();

        public ICell Find(int rowLocation, int columnLocation)
        {
            var cell = Population.Find(x => x.RowLocation.Equals(rowLocation) && x.ColumnLocation.Equals(columnLocation));
            return cell;
        }
 
    }
}