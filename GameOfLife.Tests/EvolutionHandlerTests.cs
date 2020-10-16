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
            cell.NextEvolution = "living";

            evolutionHandler.Evolve(cell);

            Assert.True(cell.IsLiving);
        }

        [Fact]
        public void EvolveShouldChangeTheCellStateToDead_GivenItsNextEvolutionIsDead()
        {
            cell.IsLiving = true;
            cell.NextEvolution = "dead";

            evolutionHandler.Evolve(cell);

            Assert.False(cell.IsLiving);
        }

    }
}