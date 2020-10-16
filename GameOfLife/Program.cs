using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var seed = new List<Cell>()
            {
                new Cell(2,1),
                new Cell(3,2),
                new Cell(1,3),
                new Cell(2,3),
                new Cell(3,3)
            };
            var evolutionHandler = new EvolutionHandler();
            var consoleParser = new ConsoleParser(new ConsoleWrapper());
            var world = new WorldGrid(rowLength: 20, columnLength: 20, initialSeed: seed, evolutionHandler: evolutionHandler, consoleParser: consoleParser);
            world.Run();
        }
    }
}

/*
Blinker 5 x 5
new Cell(2,3),
new Cell(3,3),
new Cell(4,3)

Beacon 6 x 6
new Cell(2,2),
new Cell(2,3),
new Cell(3,2),
new Cell(3,3),
new Cell(4,4),
new Cell(4,5),
new Cell(5,4),
new Cell(5,5)

Spaceships
new Cell(2,1),
new Cell(3,2),
new Cell(1,3),
new Cell(2,3),
new Cell(3,3)
*/