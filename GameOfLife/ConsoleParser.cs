using System.Collections.Generic;

namespace GameOfLife
{
    public class ConsoleParser : IConsoleParser
    {
        public IConsoleWrapper ConsoleWrapper { get; }
        public ConsoleParser(IConsoleWrapper consoleWrapper)
        {
            ConsoleWrapper = consoleWrapper;
        }

        public void DisplayWorldGrid(ICells cells, int maxColumns)
        {
            var gridString = CreateWorldGridString(cells, maxColumns);
            ConsoleWrapper.Write(gridString);
        }

        private string CreateWorldGridString(ICells cells, int maxColumns)
        {
            var gridAsString = "";
            foreach(ICell cell in cells.Population)
            {
                gridAsString += (cell.IsLiving) ? "*" : ".";
                gridAsString += (cell.ColumnLocation.Equals(maxColumns)) ? "\n" : " ";
            }
            return gridAsString;
        }
    }
}