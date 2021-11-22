using System;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            InputProcessor _inputProcessor = new InputProcessor();
            IOutput _output = new Output();
            
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
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine engine = new Engine(roverSettings, planetSettings);
            
            engine.RunProgram();
        }
    }
}