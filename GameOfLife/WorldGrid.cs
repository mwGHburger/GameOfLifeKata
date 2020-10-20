using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class WorldGrid
    {
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }
        public List<Cell> Cells { get;} = new List<Cell>();
        public IEvolutionHandler EvolutionHandler { get; }
        public ConsoleParser ConsoleParser { get; }
        public WorldGrid(int numberOfRows, int numberOfColumns, List<Cell> initialSeed, IEvolutionHandler evolutionHandler, ConsoleParser consoleParser)
        {
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
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
                ConsoleParser.DisplayWorldGrid(Cells, NumberOfColumns);
                DetermineNextEvolutionForEachCell();
                EvolveEachCell();
                System.Threading.Thread.Sleep(500);
            }
        }
        public Cell FindCell(int rowLocation, int columnLocation)
        {
            var cell = Cells.Find(x => x.RowLocation == rowLocation && x.ColumnLocation == columnLocation);
            return cell;
        }
        
        // TODO: Responsibility should be given to the EvolutionHandler Class
        public void DetermineNextEvolution(Cell cell)
        {
            var livingNeighbours = CalculateNumberOfLivingNeighbours(cell);
            var conditionForNextEvolutionToBeLiving = cell.IsLiving && (livingNeighbours == 2 || livingNeighbours == 3) || !cell.IsLiving && (livingNeighbours == 3);
            cell.isNextEvolutionLiving = conditionForNextEvolutionToBeLiving;
        }

        private void DetermineNextEvolutionForEachCell()
        {
            foreach(Cell cell in Cells)
            {
                DetermineNextEvolution(cell);
            }
        }

        private void EvolveEachCell()
        {
            foreach(Cell cell in Cells)
            {
                EvolutionHandler.Evolve(cell);
            }
        }

        private void CreateCells()
        {
            for(int rowLocation = 1; rowLocation <= NumberOfRows; rowLocation++)
            {
                for (int columnLocation = 1; columnLocation <= NumberOfColumns; columnLocation++)
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
        
        // TODO: Responsibility should be given to a NeighbourCellsHandler Class
        private int CalculateNumberOfLivingNeighbours(Cell cell)
        {
            // TODO: Can make it more efficient by storing neighbours as Cell property
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
            var neighbours = CreateCellNeighbourList(neighbourCellLocations);
            
            return CountAllLivingCell(neighbours);
        }
        private List<Cell> CreateCellNeighbourList(List<List<int>> neighbourCellLocations)
        {
            var neighbourCells = new List<Cell>();
            foreach(List<int> neighbourLocation in neighbourCellLocations)
            {
                var cellRowLocation = neighbourLocation[0];
                var cellColumnLocation = neighbourLocation[1];
                cellRowLocation = AdjustForOutOfBounds(cellRowLocation, NumberOfRows);
                cellColumnLocation = AdjustForOutOfBounds(cellColumnLocation, NumberOfColumns);
                neighbourCells.Add(FindCell(cellRowLocation, cellColumnLocation));
            }
            return neighbourCells;
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