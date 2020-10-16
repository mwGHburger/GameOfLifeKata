using System.Collections.Generic;

namespace GameOfLife
{
    public class ConsoleParser
    {
        public IConsoleWrapper ConsoleWrapper { get; }
        public ConsoleParser(IConsoleWrapper consoleWrapper)
        {
            ConsoleWrapper = consoleWrapper;
        }

        public void DisplayWorldGrid(List<Cell> cells, int maxColumns)
        {
            var gridString = CreateWorldGridString(cells, maxColumns);
            ConsoleWrapper.Write(gridString);
        }

        private string CreateWorldGridString(List<Cell> cells, int maxColumns)
        {
            var gridString = "";
            foreach(Cell cell in cells)
            {
                gridString += (cell.IsLiving) ? "*" : " ";
                gridString += (cell.Column.Location == maxColumns) ? "\n" : " ";
            }
            return gridString;
        }
    }
}