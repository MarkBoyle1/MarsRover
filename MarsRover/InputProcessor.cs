using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarsRover.Exceptions;
using MarsRover.Objectives;
using Newtonsoft.Json.Linq;

namespace MarsRover
{
    public class InputProcessor
    {
        private const string TurnLeft = "l";
        private const string TurnRight = "r";
        private const string MoveForward = "f";
        private const string MoveBack = "b";
        private const string Shoot = "s";
        private const string North = "n";
        private const string East = "e";
        private const string South = "s";
        private const string West = "w";
        private string[] DefaultCommands = new string[] {"r", "f", "f", "r", "f", "f", "l", "b"};
        private IObjective DefaultMode = new Destroyer(100);
        private DefaultSettings _defaultSettings = new DefaultSettings();
        private const string DefaultJSONFilePath = @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/JSONInput.json";
        private const string DefaultCSVFilePath = @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/MarsRoverInput.csv";
        private const string LocationTag = "location:";
        private const string ModeTag = "mode:";
        private const string CommandsTag = "commands:";
        private const string ObstaclesTag = "obstacles:";
        private const string FilePathTag = "filepath:";
        private const string GridSizeTag = "gridsize:";
        private const string MaxDistanceTag = "maxdistance:";
        private const string JSONTag = "jsonfile";
        private const string CSVTag = "csvfile";
        private const string MapObjective = "mode:map";
        private const string ExploreObjective = "mode:explore";
        private const string DestroyerObjective = "mode:destroyer";
        

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
                if (argument.StartsWith(CommandsTag))
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
                case Shoot:
                    return RoverInstruction.ShootLaser;
                default:
                    throw new InvalidInstructionException(input);
            }
        }

        private RoverLocation DetermineStartingLocation(string[] args)
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Direction directionfacing = Direction.South;
            
            foreach (var argument in args)
            {
                if (argument.StartsWith(LocationTag))
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
                    throw new InvalidDirectionException(direction);
            }
        }

        public List<Coordinate> TurnObstacleInputsIntoCoordinates(string[] args)
        {
            List<Coordinate> obstacleCoordinates = new List<Coordinate>();
            string[] obstacles = Array.Empty<string>();
            
            foreach (var argument in args)
            {
                if (argument.StartsWith(ObstaclesTag))
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
                if (argument.StartsWith(MaxDistanceTag))
                {
                     return Convert.ToInt32(argument.Remove(0,12));
                }
            }

            return DefaultSettings.DefaultMaxDistance;
        }

        private IObjective DetermineObjective(string[] args, List<Command> commands, int maxDistance)
        {
            string objective = String.Empty;
            
            foreach (var argument in args)
            {
                if (argument.StartsWith(ModeTag))
                {
                    objective = argument;
                }
            }
            
            if (objective == MapObjective)
            {
                return new MapSurface(maxDistance);
            }

            if (objective == ExploreObjective)
            {
                return new FollowCommands(commands);
            }
            
            if (objective == DestroyerObjective)
            {
                return new Destroyer(maxDistance);
            }
            

            return DefaultMode;
        }

        private int GetSizeOfGrid(string[] args)
        {
            foreach (var argument in args)
            {
                if (argument.StartsWith(GridSizeTag))
                {
                    return Convert.ToInt32(argument.Remove(0,9));
                }
            }

            return DefaultSettings.DefaultGridSize;
        }

        private IMarsSurfaceBuilder GetTypeOfBuilder(string[] args, int sizeOfGrid, List<Coordinate> obstacles)
        {
            foreach (var argument in args)
            {
                if (argument == MapObjective)
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
                if (argument.StartsWith(FilePathTag))
                {
                    filePath = argument.Remove(0,9);
                }
            }
            
            if (args.Contains(JSONTag) || Path.GetExtension(filePath) == ".json")
            {
                filePath = string.IsNullOrEmpty(filePath) ? DefaultSettings.DefaultJSONFilePath : filePath;
                
                var myJsonString = File.ReadAllText(filePath);
                var myJObject = JObject.Parse(myJsonString);
                var properties = myJObject.Properties();
            
                foreach (var p in properties)
                {
                    updatedArgs.Add(p.Name + p.Value);
                }
            }
            else if (args.Contains(CSVTag) || Path.GetExtension(filePath) == ".csv")
            {
                filePath = string.IsNullOrEmpty(filePath) ? DefaultSettings.DefaultCSVFilePath : filePath;
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