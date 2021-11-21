using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarsRover.Objectives;
using Newtonsoft.Json.Linq;

namespace MarsRover
{
    public class InputProcessor
    {
        public const string TurnLeft = "l";
        public const string TurnRight = "r";
        public const string MoveForward = "f";
        public const string MoveBack = "b";
        public const string North = "n";
        public const string East = "e";
        public const string South = "s";
        public const string West = "w";
        private string[] DefaultCommands = new string[] {"r", "f", "f", "r", "f", "f", "l", "b"};
        private int DefaultSizeOfGrid = 20;

        public RoverSettings GetRoverSettings(string[] args)
        {
            RoverLocation roverLocation = DetermineStartingLocation(args);
            List<Command> commands = GetListOfCommands(args);
            int maxDistance = GetMaxDistance(args);
            IObjective objective = DetermineObjective(args, commands, maxDistance);

            return new RoverSettings(roverLocation, commands, objective);
        }

        public PlanetSettings GetPlanetSettings(string[] args)
        {
            List<Coordinate> obstacles = TurnObstacleInputsIntoCoordinates(args);
            int sizeOfGrid = GetSizeOfGrid(args);
            IMarsSurfaceBuilder marsSurfaceBuilder = GetTypeOfBuilder(args, sizeOfGrid, obstacles);

            return new PlanetSettings(sizeOfGrid, obstacles, marsSurfaceBuilder);
        }

        private List<Command> GetListOfCommands(string[] args)
        {
            List<Command> commandList = new List<Command>();
            string[] commands = DefaultCommands;

            foreach (var argument in args)
            {
                if (argument.StartsWith("commands:"))
                {
                    commands = argument.Remove(0,9).Split(',', StringSplitOptions.RemoveEmptyEntries);
                }
            }
            
            foreach (var command in commands)
            {
                commandList.Add(TurnInputIntoCommand(command));
            }

            return commandList;
        }
        public Command TurnInputIntoCommand(string input)
        {
            RoverInstruction instruction = SelectInstruction(input);
            return new Command(instruction);
        }
        
        private RoverInstruction SelectInstruction(string input)
        {
            switch (input)
            {
                case TurnLeft:
                    return RoverInstruction.TurnLeft;
                case TurnRight:
                    return RoverInstruction.TurnRight;
                case MoveForward:
                    return RoverInstruction.MoveForward;
                case MoveBack:
                    return RoverInstruction.MoveBack;
                default:
                    throw new Exception();
            }
        }

        private RoverLocation DetermineStartingLocation(string[] args)
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Direction directionfacing = Direction.North;
            
            foreach (var argument in args)
            {
                if (argument.StartsWith("location:"))
                {
                    string[] startingLocation = argument.Remove(0, 9).Split(',', StringSplitOptions.RemoveEmptyEntries).ToArray();
                    
                    int xCoordinate = Convert.ToInt32(startingLocation[0]);
                    int yCoordinate = Convert.ToInt32(startingLocation[1]);
                    coordinate = new Coordinate(xCoordinate, yCoordinate);
                    directionfacing = DetermineDirection(startingLocation[2]);
                }
            }

            return new RoverLocation(coordinate, directionfacing);
        }

        private Direction DetermineDirection(string direction)
        {
            switch (direction)
            {
                case North:
                    return Direction.North;
                case East:
                    return Direction.East;
                case South:
                    return Direction.South;
                case West:
                    return Direction.West;
                default:
                    throw new Exception();
            }
        }

        public List<Coordinate> TurnObstacleInputsIntoCoordinates(string[] args)
        {
            List<Coordinate> obstacleCoordinates = new List<Coordinate>();
            string[] obstacles = Array.Empty<string>();
            
            foreach (var argument in args)
            {
                if (argument.StartsWith("obstacles:"))
                {
                    obstacles = argument.Remove(0,10).Split(';', StringSplitOptions.RemoveEmptyEntries);
                }
            }
            
            foreach (var obstacle in obstacles)
            {
                string[] splitInput = obstacle.Split(',');
                Coordinate coordinate = new Coordinate(Convert.ToInt32(splitInput[0]), Convert.ToInt32(splitInput[1]));
                
                obstacleCoordinates.Add(coordinate);
            }

            return obstacleCoordinates;
        }

        private int GetMaxDistance(string[] args)
        {
            foreach (var argument in args)
            {
                if (argument.StartsWith("maxdistance:"))
                {
                     return Convert.ToInt32(argument.Remove(0,12));
                }
            }

            return 100;
        }

        private IObjective DetermineObjective(string[] args, List<Command> commands, int maxDistance)
        {
            string objective = String.Empty;
            
            foreach (var argument in args)
            {
                if (argument.StartsWith("mode:"))
                {
                    objective = argument.Remove(0,5);
                }
            }
            
            foreach (var argument in args)
            {
                if (objective == "map")
                {
                    return new MapSurface(maxDistance);
                }

                if (objective == "explore")
                {
                    return new FollowCommands(commands);
                }
                
                if (objective == "destroyer")
                {
                    return new Destroyer(maxDistance);
                }
            }

            return new Destroyer(maxDistance);
        }

        private int GetSizeOfGrid(string[] args)
        {
            foreach (var argument in args)
            {
                if (argument.StartsWith("gridsize:"))
                {
                    return Convert.ToInt32(argument.Remove(0,9));
                }
            }

            return DefaultSizeOfGrid;
        }

        private IMarsSurfaceBuilder GetTypeOfBuilder(string[] args, int sizeOfGrid, List<Coordinate> obstacles)
        {
            foreach (var argument in args)
            {
                if (argument == "mode:map")
                {
                    return new MappingSurfaceBuilder(sizeOfGrid);
                }
            }

            return new MarsSurfaceBuilder(obstacles, sizeOfGrid);
        }

        public string[] GetInputFromFile(string[] args)
        {
            List<string> updatedArgs = new List<string>();
            string filePath = String.Empty;

            foreach (var argument in args)
            {
                if (argument.StartsWith("filepath:"))
                {
                    filePath = argument.Remove(0,9);
                }
            }
            
            if (args.Contains("jsonfile") || Path.GetExtension(filePath) == ".json")
            {
                filePath = string.IsNullOrEmpty(filePath) ? @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/JSONInput.json" : filePath;
                
                var myJsonString = File.ReadAllText(filePath);
                var myJObject = JObject.Parse(myJsonString);
                var properties = myJObject.Properties();
            
                foreach (var p in properties)
                {
                    updatedArgs.Add(p.Name + p.Value);
                }
            }
            else if (args.Contains("csvfile") || Path.GetExtension(filePath) == ".csv")
            {
                filePath = string.IsNullOrEmpty(filePath) ? @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/MarsRoverInput.csv" : filePath;
                args = File.ReadAllLines(filePath);
                updatedArgs = args.Select(x => x.Trim('"')).ToList();
            }

            if (updatedArgs.Count == 0)
            {
                return args;
            }

            return updatedArgs.ToArray();
        }
    }
}