using System.Collections.Generic;

namespace GameOfLife
{
    public class CellNeighbourHandler : ICellNeighbourHandler
    {
        public void FindNeighboursForEachCell(ICells cells, int numberOfRows, int numberOfColumns)
        {
            foreach(ICell cell in cells.Population)
            {
                var neighbourCellLocations = CreateListOfCellRowColumnLocations(cell);
                var neighbours = CreateListOfNeighbourCells(neighbourCellLocations, cells, numberOfRows, numberOfColumns);
                cell.Neighbours = neighbours;
            }
        }

        public int CountNumberOfLivingNeighboursOfCell(ICell cell)
        {
            var livingNeighboursCounter = 0;
            foreach(ICell neighbourCell in cell.Neighbours)
            {
                livingNeighboursCounter += (neighbourCell.IsLiving) ? 1 : 0;
            }
            return livingNeighboursCounter;
        }
        private List<ICell> CreateListOfNeighbourCells(List<List<int>> neighbourCellLocations, ICells cells, int numberOfRows, int numberOfColumns)
        {
            var neighbours = new List<ICell>();
            foreach(List<int> cellLocation in neighbourCellLocations)
            {
                var cellRowLocation = AdjustForOutOfBounds(cellLocation[0], numberOfRows);
                var cellColumnLocation = AdjustForOutOfBounds(cellLocation[1], numberOfColumns);
                var neighbourCell = cells.Find(cellRowLocation, cellColumnLocation);
                neighbours.Add(neighbourCell);
            }
            return neighbours;
        }
        private List<List<int>> CreateListOfCellRowColumnLocations(ICell cell)
        {
            var currentCellRow = cell.RowLocation;
            var currentCellColumn = cell.ColumnLocation;
            var neighbourCellLocations = new List<List<int>>()
            {
                new List<int>() {currentCellRow + 1, currentCellColumn - 1},
                new List<int>() {currentCellRow + 1, currentCellColumn},
                new List<int>() {currentCellRow + 1, currentCellColumn + 1},
                new List<int>() {currentCellRow, currentCellColumn - 1},
                new List<int>() {currentCellRow, currentCellColumn + 1},
                new List<int>() {currentCellRow - 1, currentCellColumn - 1},
                new List<int>() {currentCellRow - 1, currentCellColumn},
                new List<int>() {currentCellRow - 1, currentCellColumn + 1}
            };
            return neighbourCellLocations;
        }
        private int AdjustForOutOfBounds(int location, int length)
        {
            var firstLocation = 1;
            if(location > length)
            {
                return firstLocation;
            }
            if(location < firstLocation)
            {
                return length;
            }
            return location;
        }
    }
}