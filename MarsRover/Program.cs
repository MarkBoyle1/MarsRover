using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            InputProcessor inputProcessor = new InputProcessor();
            Console.WriteLine(inputProcessor.TurnInputIntoCommand("l").Instruction);
        }
    }
}