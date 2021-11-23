using System;
using System.IO;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            InputProcessor _inputProcessor = new InputProcessor();
            IOutput _output = new Output();
            
            //Convert input into lowercase
            args = Array.ConvertAll(args, a => a.ToLower());
            
            try
            {
                args = _inputProcessor.GetInputFromFile(args);
            }
            catch (FileNotFoundException e)
            {
                _output.DisplayMessage(e.Message);
                _output.DisplayMessage(OutputMessages.FileNotFound);
            }
            
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            Engine engine = new Engine(roverSettings, planetSettings);
            
            engine.RunProgram();
        }
    }
}