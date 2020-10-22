using System.Collections.Generic;
using Moq;
using Xunit;

namespace GameOfLife.Tests
{
    public class EvolutionHandlerTests
    {
        Mock<ICell> mockCell = new Mock<ICell>();
        Mock<ICellNeighbourHandler> mockCellNeighbourHandler = new Mock<ICellNeighbourHandler>();
        EvolutionHandler evolutionHandler = new EvolutionHandler();
        [Fact]
        public void EvolveShouldChangeTheCellStateToLiving_GivenItsNextEvolutionIsLiving()
        {
            mockCell.Setup(x => x.IsLiving).Returns(false);
            mockCell.Setup(x => x.isNextEvolutionLiving).Returns(true);

            evolutionHandler.Evolve(mockCell.Object);

            mockCell.VerifySet(x => x.IsLiving = true);
        }

        [Fact]
        public void EvolveShouldChangeTheCellStateToDead_GivenItsNextEvolutionIsDead()
        {
            mockCell.Setup(x => x.IsLiving).Returns(true);
            mockCell.Setup(x => x.isNextEvolutionLiving).Returns(false);

            evolutionHandler.Evolve(mockCell.Object);

            mockCell.VerifySet(x => x.IsLiving = false);
        }

        [Fact]
        public void DetermineNextEvolution_ShouldCalculateNumberOfLivingNeighboursOfCell()
        {
            evolutionHandler.DetermineNextEvolution(mockCellNeighbourHandler.Object,mockCell.Object);

            mockCellNeighbourHandler.Verify(x => x.CalculateNumberOfLivingNeighboursOfCell(mockCell.Object));
        }

        [Fact]
        public void LivingCellShouldDie_WhenNeighboursIsLessThan2()
        {
            mockCell.Setup(x => x.IsLiving).Returns(true);
            mockCell.SetupProperty(x => x.isNextEvolutionLiving, false);
            mockCellNeighbourHandler.Setup(x => x.CalculateNumberOfLivingNeighboursOfCell(It.IsAny<ICell>())).Returns(1);

            evolutionHandler.DetermineNextEvolution(mockCellNeighbourHandler.Object,mockCell.Object);

            mockCell.VerifySet(x => x.isNextEvolutionLiving = false);
            Assert.False(mockCell.Object.isNextEvolutionLiving);
        }

        [Fact]
        public void LivingCellShouldDie_WhenNeighboursMoreThan3()
        {
            mockCell.Setup(x => x.IsLiving).Returns(true);
            mockCell.SetupProperty(x => x.isNextEvolutionLiving, false);
            mockCellNeighbourHandler.Setup(x => x.CalculateNumberOfLivingNeighboursOfCell(It.IsAny<ICell>())).Returns(4);

            evolutionHandler.DetermineNextEvolution(mockCellNeighbourHandler.Object, mockCell.Object);

            mockCell.VerifySet(x => x.isNextEvolutionLiving = false);
            Assert.False(mockCell.Object.isNextEvolutionLiving);
        }

        [Fact]
        public void LivingCellShouldLive_WhenNeighboursIs2Or3()
        {
            mockCell.Setup(x => x.IsLiving).Returns(true);
            mockCell.SetupProperty(x => x.isNextEvolutionLiving, false);
            mockCellNeighbourHandler.Setup(x => x.CalculateNumberOfLivingNeighboursOfCell(It.IsAny<ICell>())).Returns(2);
            
            evolutionHandler.DetermineNextEvolution(mockCellNeighbourHandler.Object, mockCell.Object);

            mockCell.VerifySet(x => x.isNextEvolutionLiving = true);
            Assert.True(mockCell.Object.isNextEvolutionLiving);
        }

        [Fact]
        public void DeadCellShouldBecomeLiving_WhenNeighboursIs3()
        {
            mockCell.Setup(x => x.IsLiving).Returns(false);
            mockCell.SetupProperty(x => x.isNextEvolutionLiving, false);
            mockCellNeighbourHandler.Setup(x => x.CalculateNumberOfLivingNeighboursOfCell(It.IsAny<ICell>())).Returns(3);

            evolutionHandler.DetermineNextEvolution(mockCellNeighbourHandler.Object, mockCell.Object);

            mockCell.VerifySet(x => x.isNextEvolutionLiving = true);
            Assert.True(mockCell.Object.isNextEvolutionLiving);
        }

        [Fact]
        public void DeadCellShouldNotBecomeLiving_WhenNeighboursIs2()
        {
            mockCell.Setup(x => x.IsLiving).Returns(false);
            mockCell.SetupProperty(x => x.isNextEvolutionLiving, false);
            mockCellNeighbourHandler.Setup(x => x.CalculateNumberOfLivingNeighboursOfCell(It.IsAny<ICell>())).Returns(2);

            evolutionHandler.DetermineNextEvolution(mockCellNeighbourHandler.Object, mockCell.Object);

            mockCell.VerifySet(x => x.isNextEvolutionLiving = false);
            Assert.False(mockCell.Object.isNextEvolutionLiving);
        }

    }
}