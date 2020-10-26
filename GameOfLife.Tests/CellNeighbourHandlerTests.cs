using System.Linq;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace GameOfLife.Tests
{
    public class CellNeighbourHandlerTests
    {
        [Fact]
        public void ShouldReturnTheNumberOfLivingNeighbourForGivenCell()
        {
            var expected = 2;
            var mockCell = new Mock<ICell>();
            var mockNeighbourCell1 = new Mock<ICell>();
            var mockNeighbourCell2 = new Mock<ICell>();
            var mockNeighbourCell3 = new Mock<ICell>();
            var cellNeighbourHandler = new CellNeighbourHandler();
            var neighbours = new List<ICell>()
            {
                mockNeighbourCell1.Object,
                mockNeighbourCell2.Object,
                mockNeighbourCell3.Object
            };

            mockCell.Setup(x => x.Neighbours).Returns(neighbours);
            mockNeighbourCell1.Setup(x => x.IsLiving).Returns(true);
            mockNeighbourCell2.Setup(x => x.IsLiving).Returns(true);
            mockNeighbourCell3.Setup(x => x.IsLiving).Returns(false);

            var actual = cellNeighbourHandler.CountNumberOfLivingNeighboursOfCell(mockCell.Object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldReturnAListOfNeighbourCellsForGivenCell()
        {
            var mockCells = new Mock<ICells>();
            var mockCell1 = new Mock<ICell>();
            var mockCell2 = new Mock<ICell>();
            var mockCell3 = new Mock<ICell>();
            var mockCell4 = new Mock<ICell>();
            var population = new List<ICell>()
            {
                mockCell1.Object,
                mockCell2.Object,
                mockCell3.Object,
                mockCell4.Object
            };
            var cellNeighbourHandler = new CellNeighbourHandler();
            var numberOfRows = 2;
            var numberOfColumns = 2;

            mockCells.Setup(x => x.Population).Returns(population);
     
            cellNeighbourHandler.FindNeighboursForEachCell(mockCells.Object, numberOfRows, numberOfColumns);

            mockCells.VerifyGet(x => x.Population);
            mockCell1.VerifyGet(x => x.RowLocation);
            mockCell1.VerifyGet(x => x.ColumnLocation);
            mockCell1.VerifySet(x => x.Neighbours = It.IsAny<List<ICell>>());            
        }

        [Fact]
        public void ShouldReturnAListOfNeighbourCellsForCellAtEdgeOfGride()
        {
            var startLocation = 1;
            var numberOfRows = 4;
            var numberOfColumns = 4;
            var cells = new Cells();
            SetupCellPopulation(cells, numberOfRows, numberOfColumns);
            var selectedCell = cells.Find(1,1);
            var cellNeighbourHandler = new CellNeighbourHandler();
            
            cellNeighbourHandler.FindNeighboursForEachCell(cells, numberOfRows, numberOfColumns);
            
            foreach(ICell neighbour in selectedCell.Neighbours)
            {
                Assert.InRange(neighbour.RowLocation,startLocation,numberOfRows);
                Assert.InRange(neighbour.ColumnLocation,startLocation,numberOfColumns);
            }
                        
        }

        private void SetupCellPopulation(ICells cells, int numberOfRows, int numberOfColumns)
        {
            var mockCells = new Mock<ICells>();
            for(int rowLocation = 1; rowLocation <= numberOfRows; rowLocation++)
            {
                for(int columnLocation = 1; columnLocation <= numberOfColumns; columnLocation++)
                {
                    var cell = new Cell(rowLocation, columnLocation);
                    cells.Population.Add(cell);
                }
            }
        }
    }
}