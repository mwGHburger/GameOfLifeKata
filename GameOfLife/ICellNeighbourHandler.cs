namespace GameOfLife
{
    public interface ICellNeighbourHandler
    {
        void FindNeighboursForEachCell(ICells cells, int numberOfRows, int numberOfColumns);
        int CalculateNumberOfLivingNeighboursOfCell(ICell cell);
         
    }
}