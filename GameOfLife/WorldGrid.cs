using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class WorldGrid
    {
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }
        public ICells Cells { get; }
        public IEvolutionHandler EvolutionHandler { get; }
        public IConsoleParser ConsoleParser { get; }
        public ICellNeighbourHandler CellNeighbourHandler { get; }
        private bool _running = true;

        public WorldGrid(int numberOfRows, int numberOfColumns, List<ICell> initialSeed, IEvolutionHandler evolutionHandler, IConsoleParser consoleParser, ICellNeighbourHandler cellNeighbourHandler, ICells cells)
        {
            EvolutionHandler = evolutionHandler;
            ConsoleParser = consoleParser;
            CellNeighbourHandler = cellNeighbourHandler;
            Cells = cells;
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
            CreateCells();
            PlantSeed(initialSeed);
            CellNeighbourHandler.FindNeighboursForEachCell(Cells, NumberOfRows, NumberOfColumns);
        }
        public void Run()
        {
            while(_running)
            {
                Console.Clear();
                ConsoleParser.DisplayWorldGrid(Cells, NumberOfColumns);
                DetermineNextEvolutionForEachCell();
                EvolveEachCell();
                System.Threading.Thread.Sleep(500);
            }
        }
        private void CreateCells()
        {
            var startLocation = 1;
            for(int rowLocation = startLocation; rowLocation <= NumberOfRows; rowLocation++)
            {
                for (int columnLocation = startLocation; columnLocation <= NumberOfColumns; columnLocation++)
                {
                    var cell = new Cell(rowLocation: rowLocation, columnLocation: columnLocation);
                    Cells.Population.Add(cell);
                }
            }
        }
        private void PlantSeed(List<ICell> initialSeed)
        {
            foreach(ICell cell in initialSeed)
            {
                var selectedCell = Cells.Find(cell.RowLocation, cell.ColumnLocation);
                selectedCell.IsLiving = true;
            }
        }
        private void DetermineNextEvolutionForEachCell()
        {
            foreach(Cell cell in Cells.Population)
            {
                EvolutionHandler.DetermineNextEvolution(CellNeighbourHandler, cell);
            }
        }
        private void EvolveEachCell()
        {
            foreach(Cell cell in Cells.Population)
            {
                EvolutionHandler.Evolve(cell);
            }
        }
    }
}