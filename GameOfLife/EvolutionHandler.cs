namespace GameOfLife
{
    public class EvolutionHandler : IEvolutionHandler
    {
        public void Evolve(Cell cell)
        {
            if (cell.isNextEvolutionLiving)
            {
                cell.IsLiving = true;
                return;
            }   
            else
            {
                cell.IsLiving = false;
                return;
            }
        }
    }
}