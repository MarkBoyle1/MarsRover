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
            
            args = Array.ConvertAll(args, a => a.ToLower());
            
            try
            {
                args = _inputProcessor.GetInputFromFile(args);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(3000);
                Console.WriteLine("File Not Found!");
                Thread.Sleep(5000);
            }
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine engine = new Engine(roverSettings, planetSettings);
            
            engine.RunProgram();
        }
    }
}