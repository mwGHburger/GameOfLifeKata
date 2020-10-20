using Xunit;

namespace GameOfLife.Tests
{
    public class EvolutionHandlerTests
    {
        EvolutionHandler evolutionHandler = new EvolutionHandler();
        Cell cell = new Cell(2,2);
        [Fact]
        public void EvolveShouldChangeTheCellStateToLiving_GivenItsNextEvolutionIsLiving()
        {
            cell.IsLiving = false;
            cell.isNextEvolutionLiving = true;

            evolutionHandler.Evolve(cell);

            Assert.True(cell.IsLiving);
        }

        [Fact]
        public void EvolveShouldChangeTheCellStateToDead_GivenItsNextEvolutionIsDead()
        {
            cell.IsLiving = true;
            cell.isNextEvolutionLiving = false;

            evolutionHandler.Evolve(cell);

            Assert.False(cell.IsLiving);
        }

    }
}