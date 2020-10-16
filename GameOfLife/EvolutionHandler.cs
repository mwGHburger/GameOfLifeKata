namespace GameOfLife
{
    public class EvolutionHandler : IEvolutionHandler
    {
        public void Evolve(Cell cell)
        {
            if (cell.NextEvolution == "living")
            {
                cell.IsLiving = true;
                return;
            }   
            
            if(cell.NextEvolution == "dead")
            {
                cell.IsLiving = false;
                return;
            }
        }
    }
}