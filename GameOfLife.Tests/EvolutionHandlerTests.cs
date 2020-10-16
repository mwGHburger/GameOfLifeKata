using Xunit;

namespace GameOfLife.Tests
{
    public class EvolutionHandlerTests
    {
        [Fact]
        public void EvolveShouldChangeTheCellStateToLiving_GivenItsNextEvolutionIsLiving()
        {
            var cell = new Cell(2,2);
            cell.IsLiving = false;
            cell.NextEvolution = "living";
            var evolutionHandler = new EvolutionHandler();

            evolutionHandler.Evolve(cell);

            Assert.True(cell.IsLiving);
        }

        [Fact]
        public void EvolveShouldChangeTheCellStateToDead_GivenItsNextEvolutionIsDead()
        {
            var cell = new Cell(2,2);
            cell.IsLiving = true;
            cell.NextEvolution = "dead";
            var evolutionHandler = new EvolutionHandler();

            evolutionHandler.Evolve(cell);

            Assert.False(cell.IsLiving);
        }

    }
}