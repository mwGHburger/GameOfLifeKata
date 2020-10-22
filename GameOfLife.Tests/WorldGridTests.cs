using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace GameOfLife.Tests
{
    public class WorldGridTests
    {
        List<ICell> defaultSeed = new List<ICell>()
        {
            new Cell(2,2),
        };
        

        [Fact]
        public void ShouldCreateRowsandColumnsInWorldGrid()
        {   
            var world = SetupWorldGrid(8,8, defaultSeed);
            
            Assert.Equal(8, world.NumberOfRows);
            Assert.Equal(8, world.NumberOfColumns);
        }

        [Fact]
        public void ShouldCreatCellsInWorldGrid()
        {
            var world = SetupWorldGrid(6,6,defaultSeed);
            var cellPopulation = world.Cells.Population;

            Assert.Equal(36, cellPopulation.Count);
        }
        
        [Fact]
        public void ShouldPlantSeedIntoWorld()
        {
            var seed = new List<ICell>()
            {
                new Cell(2,2),
                new Cell(2,3),
                new Cell(3,2),
                new Cell(3,3)
            };
            var world = SetupWorldGrid(4,4, seed);
            var cells = world.Cells;
            var cell_1 = cells.Find(2,2);
            var cell_2 = cells.Find(2,3);
            var cell_3 = cells.Find(3,2);
            var cell_4 = cells.Find(3,3);
            var cell_5 = cells.Find(1,1);

            Assert.True(cell_1.IsLiving);
            Assert.True(cell_2.IsLiving);
            Assert.True(cell_3.IsLiving);
            Assert.True(cell_4.IsLiving);
            Assert.False(cell_5.IsLiving);
        }

        private WorldGrid SetupWorldGrid(int numberOfRows, int numberOfColumns, List<ICell> initialSeed)
        {
            var mockEvolutionHandler = new Mock<IEvolutionHandler>();
            var mockConsoleParser = new Mock<IConsoleParser>();
            var mockCellNeighbourHandler = new Mock<ICellNeighbourHandler>();
            // var mockCells = new Mock<ICells>();
            var cells = new Cells();

            var world = new WorldGrid(
                numberOfRows: numberOfRows, 
                numberOfColumns: numberOfColumns, 
                initialSeed: initialSeed, 
                evolutionHandler: mockEvolutionHandler.Object, 
                consoleParser: mockConsoleParser.Object, 
                cellNeighbourHandler: mockCellNeighbourHandler.Object, 
                cells: cells
            );

            return world;
        }

    }
}
