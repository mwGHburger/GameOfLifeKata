using System;

namespace GameOfLife
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void Write(string input)
        {
            Console.WriteLine(input);
        }
    }
}