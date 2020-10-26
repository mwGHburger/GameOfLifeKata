namespace GameOfLife
{
    public interface IEvolutionHandler
    {
         void DetermineNextEvolution(ICellNeighbourHandler cellNeighbourHandler, ICell cell);
         void Evolve(ICell cell);
    }
}