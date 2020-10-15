using System.Collections.Generic;

namespace GameOfLife
{
    public class WorldGrid
    {
        public List<Row> Rows { get; } = new List<Row>();
        public List<Column> Columns { get;} = new List<Column>();
        public List<Cell> Cells { get;} = new List<Cell>();
        public WorldGrid(int rowLength, int columnLength, List<Cell> initialSeed)
        {
            CreateRows(rowLength);
            CreateColumns(columnLength);
            CreateCells();
            PlantSeed(initialSeed);
        }
        public Cell FindCell(int rowLocation, int columnLocation)
        {
            var cell = Cells.Find(x => x.RowLocation == rowLocation && x.ColumnLocation == columnLocation);
            return cell;
        }
        public void DetermineNextEvolution(Cell cell)
        {
            var livingNeighbours = CalculateNumberOfLivingNeighbours(cell);
            var conditionForNextEvolutionToBeLiving = cell.IsLiving && (livingNeighbours == 2 || livingNeighbours == 3) || !cell.IsLiving && (livingNeighbours == 3);

            if(conditionForNextEvolutionToBeLiving)
            {
                cell.NextEvolution = "living";
                return;
            }
            cell.NextEvolution = "dead";
            return;
        }

        private void CreateRows(int rowLength)
        {
            for(int i = 1; i <= rowLength; i++)
            {
                Rows.Add(new Row(location: i));
            }
        }

        private void CreateColumns(int columnLength)
        {
            for(int i = 1; i <= columnLength; i++)
            {
                Columns.Add(new Column(location: i));
            }
        }

        private void CreateCells()
        {
            foreach(Row row in Rows)
            {
                foreach (Column column in Columns)
                {
                    Cells.Add(new Cell(row: row, column: column));
                }
            }
        }

        private void PlantSeed(List<Cell> initialSeed)
        {
            foreach(Cell cell in initialSeed)
            {
                var selectedCell = FindCell(cell.RowLocation, cell.ColumnLocation);
                selectedCell.IsLiving = true;
            }
        }
        
        // TODO: Can move this out to its own class, there are too many responsibilities in this class
        private int CalculateNumberOfLivingNeighbours(Cell cell)
        {
            // TODO: Can make it more efficient by storing neighbours as Cell property
            var neighbours = new List<Cell>();
            AddNeighboursAboveRowToList(cell.RowLocation,cell.ColumnLocation, neighbours);
            AddNeighboursSameRowToList(cell.RowLocation,cell.ColumnLocation, neighbours);
            AddNeighboursBelowRowToList(cell.RowLocation,cell.ColumnLocation, neighbours);
            return CountAllLivingCell(neighbours);
        }
        private void AddNeighboursAboveRowToList(int currentCellRowLocation, int currentCellColumnLocation, List<Cell> neighbourCells)
        {
            // Fix this out of bounds logic
            System.Console.WriteLine("Hit");
            if(currentCellRowLocation + 1 > Rows.Count)
            {
                currentCellRowLocation = 0;
            }
            else if(currentCellRowLocation - 1 < 1)
            {
                currentCellRowLocation = Rows.Count + 1;
            }

            if(currentCellColumnLocation + 1 > Columns.Count)
            {
                currentCellColumnLocation = 0;
            }
            else if(currentCellColumnLocation - 1 < 1)
            {
                currentCellColumnLocation = Columns.Count + 1;
            }
            System.Console.WriteLine($"Row should be 0: {currentCellRowLocation}");
            System.Console.WriteLine($"Column should is : {currentCellColumnLocation}");
            neighbourCells.Add(FindCell(currentCellRowLocation + 1, currentCellColumnLocation - 1));
            neighbourCells.Add(FindCell(currentCellRowLocation + 1, currentCellColumnLocation));
            neighbourCells.Add(FindCell(currentCellRowLocation + 1, currentCellColumnLocation + 1));
        }
        private void AddNeighboursSameRowToList(int currentCellRowLocation, int currentCellColumnLocation, List<Cell> neighbourCells)
        {
            neighbourCells.Add(FindCell(currentCellRowLocation, currentCellColumnLocation - 1));
            neighbourCells.Add(FindCell(currentCellRowLocation, currentCellColumnLocation + 1));
        }
        private void AddNeighboursBelowRowToList(int currentCellRowLocation, int currentCellColumnLocation, List<Cell> neighbourCells)
        {
            neighbourCells.Add(FindCell(currentCellRowLocation - 1, currentCellColumnLocation - 1));
            neighbourCells.Add(FindCell(currentCellRowLocation - 1, currentCellColumnLocation));
            neighbourCells.Add(FindCell(currentCellRowLocation - 1, currentCellColumnLocation + 1));
        }
        private int CountAllLivingCell(List<Cell> neighbourCells)
        {
            var livingNeighboursCounter = 0;
            foreach(Cell neighbourCell in neighbourCells)
            {
                if(neighbourCell.IsLiving)
                {
                    livingNeighboursCounter++;
                }
            }
            return livingNeighboursCounter;
        }

    }
}