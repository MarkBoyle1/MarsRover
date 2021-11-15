
using System;
using System.Text;
using Microsoft.VisualBasic;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            InputProcessor _inputProcessor = new InputProcessor();
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine engine = new Engine(roverSettings, planetSettings);
            
            engine.RunProgram();
        }
    }
}