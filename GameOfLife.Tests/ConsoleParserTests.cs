using System.Net.Mail;
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
            var numberOfRows = 4;
            var numberOfColumns = 4;
            var cellList = SetupCellList(numberOfRows, numberOfColumns);
            var mockCells = SetupMockCells(cellList);
            var mockConsoleWrapper = new Mock<IConsoleWrapper>();
            var consolParser = new ConsoleParser(mockConsoleWrapper.Object);

            consolParser.DisplayWorldGrid(mockCells.Object, numberOfColumns);

            mockConsoleWrapper.Verify(x => x.Write(". . . .\n. * * .\n. * * .\n. . . .\n"));
        }

        private Mock<ICells> SetupMockCells(List<ICell> cellList)
        {
            var cells = new Mock<ICells>();
            cells.Setup(x => x.Population).Returns(cellList);
            return cells;
        }

        private List<ICell> SetupCellList(int numberOfRows, int numberOfColumns)
        {
            var cellList = new List<ICell>();
            for(int rowLocation = 1; rowLocation <= numberOfRows; rowLocation++)
            {
                for(int columnLocation = 1; columnLocation <= numberOfColumns; columnLocation++)
                {
                    var mockCell = new Mock<ICell>();
                    mockCell.Setup(x => x.RowLocation).Returns(rowLocation);
                    mockCell.Setup(x => x.ColumnLocation).Returns(columnLocation);
                    var isLiving = ((rowLocation.Equals(2) || rowLocation.Equals(3)) && (columnLocation.Equals(2) || columnLocation.Equals(3)));
                    mockCell.Setup(x => x.IsLiving).Returns(isLiving);
                    cellList.Add(mockCell.Object);
                }
            }
            return cellList;
        }
    }
}