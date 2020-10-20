using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class WorldGrid
    {
        public int RowLength { get; }
        public int ColumnLength { get; }
        public List<Cell> Cells { get;} = new List<Cell>();
        public IEvolutionHandler EvolutionHandler { get; }
        public ConsoleParser ConsoleParser { get; }
        public WorldGrid(int rowLength, int columnLength, List<Cell> initialSeed, IEvolutionHandler evolutionHandler, ConsoleParser consoleParser)
        {
            RowLength = rowLength;
            ColumnLength = columnLength;
            CreateCells();
            PlantSeed(initialSeed);
            EvolutionHandler = evolutionHandler;
            ConsoleParser = consoleParser;
        }

        public void Run()
        {
            while(true)
            {
                Console.Clear();
                ConsoleParser.DisplayWorldGrid(Cells, ColumnLength);
                // TODO: DRY issue
                foreach(Cell cell in Cells)
                {
                    DetermineNextEvolution(cell);
                }

                foreach(Cell cell in Cells)
                {
                    EvolutionHandler.Evolve(cell);
                }
                System.Threading.Thread.Sleep(500);
            }
        }
        public Cell FindCell(int rowLocation, int columnLocation)
        {
            var cell = Cells.Find(x => x.RowLocation == rowLocation && x.ColumnLocation == columnLocation);
            return cell;
        }
        public void DetermineNextEvolution(Cell cell)
        {
            var livingNeighbours = CalculateNumberOfLivingNeighbours(cell);
            var conditionForNextEvolutionToBeLiving = cell.IsLiving && (livingNeighbours == 2 || livingNeighbours == 3) || !cell.IsLiving && (livingNeighbours == 3);
            cell.isNextEvolutionLiving = conditionForNextEvolutionToBeLiving;
        }

        private void CreateCells()
        {
            for(int rowLocation = 1; rowLocation <= RowLength; rowLocation++)
            {
                for (int columnLocation = 1; columnLocation <= ColumnLength; columnLocation++)
                {
                    Cells.Add(new Cell(rowLocation: rowLocation, columnLocation: columnLocation));
                }
            }
        }

        private void PlantSeed(List<Cell> initialSeed)
        {
            foreach(Cell cell in initialSeed)
            {
                var selectedCell = FindCell(cell.RowLocation, cell.ColumnLocation);
                selectedCell.IsLiving = true;
            }
        }
        
        // TODO: Can move this out to its own class, there are too many responsibilities in this class
        private int CalculateNumberOfLivingNeighbours(Cell cell)
        {
            // TODO: Can make it more efficient by storing neighbours as Cell property
            // TODO: make neighbouring cells into a list and make the addneighbours method into one method that takes the list
            var neighbours = new List<Cell>();
            AddNeighboursAboveRowToList(cell.RowLocation,cell.ColumnLocation, neighbours);
            AddNeighboursSameRowToList(cell.RowLocation,cell.ColumnLocation, neighbours);
            AddNeighboursBelowRowToList(cell.RowLocation,cell.ColumnLocation, neighbours);
            return CountAllLivingCell(neighbours);
        }
        private void AddNeighboursAboveRowToList(int currentCellRow, int currentCellColumn, List<Cell> neighbourCells)
        {
            AddCellToNeighbourList(currentCellRow + 1, currentCellColumn - 1, neighbourCells);
            AddCellToNeighbourList(currentCellRow + 1, currentCellColumn, neighbourCells);
            AddCellToNeighbourList(currentCellRow + 1, currentCellColumn + 1, neighbourCells);
        }
        private void AddNeighboursSameRowToList(int currentCellRow, int currentCellColumn, List<Cell> neighbourCells)
        {
            AddCellToNeighbourList(currentCellRow, currentCellColumn - 1, neighbourCells);
            AddCellToNeighbourList(currentCellRow, currentCellColumn + 1, neighbourCells);
        }
        private void AddNeighboursBelowRowToList(int currentCellRow, int currentCellColumn, List<Cell> neighbourCells)
        {
            AddCellToNeighbourList(currentCellRow - 1, currentCellColumn - 1, neighbourCells);
            AddCellToNeighbourList(currentCellRow - 1, currentCellColumn, neighbourCells);
            AddCellToNeighbourList(currentCellRow - 1, currentCellColumn + 1, neighbourCells);
        }
        private int CountAllLivingCell(List<Cell> neighbourCells)
        {
            var livingNeighboursCounter = 0;
            foreach(Cell neighbourCell in neighbourCells)
            {
                livingNeighboursCounter += (neighbourCell.IsLiving) ? 1 : 0;
            }
            return livingNeighboursCounter;
        }

        private void AddCellToNeighbourList(int currentCellRow, int currentCellColumn, List<Cell> neighbourCells)
        {
            currentCellRow = AdjustForOutOfBoundsRow(currentCellRow);
            currentCellColumn = AdjustForOutOfBoundsColumn(currentCellColumn);
            neighbourCells.Add(FindCell(currentCellRow, currentCellColumn));
        }

        // Can refactor for DRY
        private int AdjustForOutOfBoundsRow(int rowLocation)
        {
            if(rowLocation > RowLength)
            {
                return 1;
            }
            
            if(rowLocation < 1)
            {
                return RowLength;
            }

            return rowLocation;
        }

        private int AdjustForOutOfBoundsColumn(int columnLocation)
        {
            if(columnLocation > ColumnLength)
            {
                return 1;
            }
            
            if(columnLocation < 1)
            {
                return ColumnLength;
            }

            return columnLocation;
        }

    }
}