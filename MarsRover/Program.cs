using System;
using System.Collections.Generic;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            InputProcessor inputProcessor = new InputProcessor();
            Engine _engine = new Engine();
            string[] startingPoint = new[] {"1", "1", "N"};
            string[] commands = new[] {"r", "f", "r", "f", "f", "l", "b"};
            Coordinate obstacle1 = new Coordinate(2, 2);
            List<Coordinate> obstacles = new List<Coordinate>(){obstacle1};
            
            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
        }
    }
}