using System;
using System.Collections.Generic;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine _engine = new Engine();
            string[] startingPoint;
            
            if (args.Length == 3)
            {
                startingPoint = args;
            }
            else
            {
                startingPoint = new[] {"1", "1", "N"};
            }
            
            
            string[] commands = new[] {"r", "f", "f", "r", "f", "f", "l", "b"};
            string[] obstacles = Array.Empty<string>();

            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
        }
    }
}