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
                // TODO: DRY issue - can refactor this
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
                var cellRow = AdjustForOutOfBounds(neighbourLocation[0], RowLength);
                var cellColumn = AdjustForOutOfBounds(neighbourLocation[1], ColumnLength);
                neighbourCells.Add(FindCell(cellRow, cellColumn));
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