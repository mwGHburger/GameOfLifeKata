namespace GameOfLife
{
    public class EvolutionHandler : IEvolutionHandler
    {
        public void DetermineNextEvolution(ICellNeighbourHandler cellNeighbourHandler, ICell cell)
        {
            var livingNeighbours = cellNeighbourHandler.CalculateNumberOfLivingNeighboursOfCell(cell);
            var conditionForNextEvolutionToBeLiving = cell.IsLiving && (livingNeighbours == 2 || livingNeighbours == 3) || !cell.IsLiving && (livingNeighbours == 3);
            cell.isNextEvolutionLiving = conditionForNextEvolutionToBeLiving;
        }
        public void Evolve(ICell cell)
        {
            cell.IsLiving = cell.isNextEvolutionLiving;
        }
    }
}