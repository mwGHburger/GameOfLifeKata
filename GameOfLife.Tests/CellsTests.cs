using Moq;
using Xunit;

namespace GameOfLife.Tests
{
    public class CellsTests
    {
        [Fact]
        public void ShouldReturnCellGivenRowAndColumnLocation()
        {
            var cells = new Cells();
            var numberOfRows = 4;
            var numberOfColumns = 4;
            var selectedRow = 2;
            var selectedColumn = 2;

            SetupCellPopulation(cells, numberOfRows, numberOfColumns);

            var actual = cells.Find(selectedRow, selectedColumn);

            Assert.Equal(selectedRow, actual.RowLocation);
            Assert.Equal(selectedColumn, actual.ColumnLocation);
        }

        private void SetupCellPopulation(Cells cells, int numberOfRows, int numberOfColumns)
        {
            for(int rowLocation = 1; rowLocation <= numberOfRows; rowLocation++)
            {
                for(int columnLocation = 1; columnLocation <= numberOfColumns; columnLocation++)
                {
                    var mockCell = new Mock<ICell>();
                    mockCell.Setup(x => x.RowLocation).Returns(rowLocation);
                    mockCell.Setup(x => x.ColumnLocation).Returns(columnLocation);
                    cells.Population.Add(mockCell.Object);
                }
            }
        }
    }
}