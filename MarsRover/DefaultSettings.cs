using System.Collections.Generic;
using MarsRover.Objectives;

namespace MarsRover
{
    public class DefaultSettings
    {
        public RoverLocation DefaultLocation;
        public string[] DefaultCommands;
        public List<Coordinate> DefaultObstacles;
        public IObjective DefaultMode;
        public const int DefaultMaxDistance = 100;
        public const int DefaultGridSize = 20;
        public const int DefaultPercentageOfObstacles = 20;
        public const string DefaultJSONFilePath = @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/JSONInput.json";
        public const string DefaultCSVFilePath = @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/MarsRoverInput.csv";

        public DefaultSettings()
        {
            // DefaultLocation = new RoverLocation(new Coordinate(1,1), Direction.North);
            DefaultCommands = new string[] {"r", "f", "f", "r", "f", "f", "l", "b"};
            DefaultObstacles = new List<Coordinate>();
            DefaultMode = new Destroyer(DefaultMaxDistance);
        }
    }
}