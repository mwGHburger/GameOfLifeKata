namespace GameOfLife
{
    public class EvolutionHandler : IEvolutionHandler
    {
        public void DetermineNextEvolution(ICellNeighbourHandler cellNeighbourHandler, ICell cell)
        {
            var livingNeighbours = cellNeighbourHandler.CountNumberOfLivingNeighboursOfCell(cell);
            var conditionForLivingCellToLive = cell.IsLiving && (livingNeighbours.Equals(2) || livingNeighbours.Equals(3));
            var conditionForDeadCellToLive = !cell.IsLiving && (livingNeighbours.Equals(3));
            cell.isNextEvolutionLiving = conditionForLivingCellToLive || conditionForDeadCellToLive;
        }
        public void Evolve(ICell cell)
        {
            cell.IsLiving = cell.isNextEvolutionLiving;
        }
    }
}