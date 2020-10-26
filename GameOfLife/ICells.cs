using System.Collections.Generic;

namespace GameOfLife
{
    public interface ICells
    {
        List<ICell> Population { get; }
        ICell Find(int rowLocation, int columnLocation);
    }
}