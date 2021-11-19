using System.IO;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultFilePath = @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/MarsRoverInput.csv";

            if (args.Contains("file"))
            {
                args = File.ReadAllLines(defaultFilePath);
                args = args.Select(x => x.Trim('"')).ToArray();
            }
            
            InputProcessor _inputProcessor = new InputProcessor();
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine engine = new Engine(roverSettings, planetSettings);
            
            engine.RunProgram();
        }
    }
}