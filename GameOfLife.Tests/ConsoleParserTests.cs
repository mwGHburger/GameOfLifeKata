using System.Collections.Generic;
using Moq;
using Xunit;

namespace GameOfLife.Tests
{
    public class ConsoleParserTests
    {
        [Fact]
        public void ShouldDisplayLivingAndDeadCells()
        {
            var seed = new List<Cell>()
            {
                new Cell(2,2),
                new Cell(2,3),
                new Cell(3,2),
                new Cell(3,3)
            };
            var mockEvolutionHandler = new Mock<IEvolutionHandler>();
            var consoleParser = new ConsoleParser(new ConsoleWrapper());
            var world = new WorldGrid(rowLength: 4, columnLength: 4, initialSeed: seed, evolutionHandler: mockEvolutionHandler.Object, consoleParser: consoleParser);
            var mockConsoleWrapper = new Mock<IConsoleWrapper>();
            var consolParser = new ConsoleParser(mockConsoleWrapper.Object);

            consolParser.DisplayWorldGrid(world.Cells, world.Columns.Count);

            mockConsoleWrapper.Verify(x => x.Write("       \n  * *  \n  * *  \n       \n"));
        }
    }
}