using System;
using System.Collections.Generic;
using Xunit;

namespace GameOfLife.Tests
{
    public class WorldGridTests
    {
        List<Cell> defaultSeed = new List<Cell>()
            {
                new Cell(2,2),
            };
        [Fact]
        public void ShouldCreateRowsandColumnsInWorldGrid()
        { 
            var world = new WorldGrid(rowLength: 5, columnLength: 8, initialSeed: defaultSeed);
            
            Assert.Equal(5, world.Rows.Count);
            Assert.Equal(8, world.Columns.Count);
        }

        [Fact]
        public void ShouldCreatCellsInWorldGrid()
        {
            var world = new WorldGrid(rowLength: 6, columnLength: 6, initialSeed: defaultSeed);

            Assert.Equal(36, world.Cells.Count);
        }

        [Theory]
        [InlineData(4,4,2,3,2,3)]
        public void ShouldReturnCell_GivenRowAndColumn(int rowLength, int columnLength, int rowLocation, int columnLocation, int expectedRowLocation, int expectedColumnLocation)
        {
            var world = new WorldGrid(rowLength: rowLength, columnLength: columnLength, initialSeed: defaultSeed);

            var actual = world.FindCell(rowLocation,columnLocation);

            Assert.IsType<Cell>(actual);
            Assert.Equal(expectedRowLocation, actual.RowLocation);
            Assert.Equal(expectedColumnLocation, actual.ColumnLocation);
        }
        
        [Fact]
        public void ShouldPlantSeedIntoWorld()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,2),
                new Cell(2,3),
                new Cell(3,2),
                new Cell(3,3)
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            var cell_1 = world.FindCell(2,2);
            var cell_2 = world.FindCell(2,3);
            var cell_3 = world.FindCell(3,2);
            var cell_4 = world.FindCell(3,3);
            var cell_5 = world.FindCell(1,1);

            Assert.True(cell_1.IsLiving);
            Assert.True(cell_2.IsLiving);
            Assert.True(cell_3.IsLiving);
            Assert.True(cell_4.IsLiving);
            Assert.False(cell_5.IsLiving);
        }

        // [Fact]
        public void LivingCellShouldDie_WhenNeighboursIsLessThan2()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,2),
                new Cell(2,3),
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(2,2);
            
            Assert.True(cell.IsLiving);

            world.DetermineNextEvolution(cell);

            Assert.Equal("dead", cell.NextEvolution);
        }

        // [Fact]
        public void LivingCellShouldDie_WhenNeighboursMoreThan3()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,2),
                new Cell(2,3),
                new Cell(3,1),
                new Cell(3,2),
                new Cell(3,3)
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(2,2);
            
            Assert.True(cell.IsLiving);

            world.DetermineNextEvolution(cell);

            Assert.Equal("dead", cell.NextEvolution);
        }

        // [Fact]
        public void LivingCellShouldLive_WhenNeighboursIs2Or3()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,2),
                new Cell(2,3),
                new Cell(3,2),
                new Cell(3,3)
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(2,2);
            
            Assert.True(cell.IsLiving);
            
            world.DetermineNextEvolution(cell);

            Assert.Equal("living", cell.NextEvolution);
        }

        // [Fact]
        public void DeadCellShouldBecomeLiving_WhenNeighboursIs3()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,3),
                new Cell(3,2),
                new Cell(3,3)
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(2,2);
            
            Assert.False(cell.IsLiving);
            
            world.DetermineNextEvolution(cell);

            Assert.Equal("living", cell.NextEvolution);
        }

        // [Fact]
        public void DeadCellShouldNotBecomeLiving_WhenNeighboursIs2()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,3),
                new Cell(3,2),
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(2,2);
            
            Assert.False(cell.IsLiving);
            
            world.DetermineNextEvolution(cell);

            Assert.Equal("dead", cell.NextEvolution);
        }

        // [Fact]
        public void LivingCellShouldLive_WhenNeighboursIs2Or3_AndRowOutOfBounds()
        {
            var seed = new List<Cell>()
            {
                new Cell(1,2),
                new Cell(1,3),
                new Cell(4,2),
                new Cell(4,3)
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(4,2);
            
            Assert.True(cell.IsLiving);

            world.DetermineNextEvolution(cell);

            Assert.Equal("living", cell.NextEvolution);
        }

        [Fact]
        public void DeadCellShouldBecomeLiving_WhenNeighboursIs3_AndRowOutOfBounds()
        {
            var seed = new List<Cell>()
            {
                new Cell(4,3),
                new Cell(3,3),
                new Cell(3,4)
            };
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed);
            
            var cell = world.FindCell(4,4);
            
            Assert.False(cell.IsLiving);
            
            world.DetermineNextEvolution(cell);

            Assert.Equal("living", cell.NextEvolution);
        }
    }
}
