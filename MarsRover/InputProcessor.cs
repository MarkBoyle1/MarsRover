using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Objectives;

namespace MarsRover
{
    public class InputProcessor
    {
        public const string TurnLeft = "l";
        public const string TurnRight = "r";
        public const string MoveForward = "f";
        public const string MoveBack = "b";
        public const string North = "N";
        public const string East = "E";
        public const string South = "S";
        public const string West = "W";
        private string[] DefaultCommands = new string[] {"r", "f", "f", "r", "f", "f", "l", "b"};

        public RoverSettings GetRoverSettings(string[] args)
        {
            RoverLocation roverLocation = DetermineStartingLocation(args);
            List<Command> commands = GetListOfCommands(args);
            IObjective objective = DetermineObjective(args, commands);

            return new RoverSettings(roverLocation, commands, objective);
        }

        public PlanetSettings GetPlanetSettings(string[] args)
        {
            List<Coordinate> obstacles = TurnObstacleInputsIntoCoordinates(args);
            int sizeOfGrid = GetSizeOfGrid(args);
            IMarsSurfaceBuilder marsSurfaceBuilder = GetTypeOfBuilder(args, sizeOfGrid, obstacles);

            return new PlanetSettings(sizeOfGrid, obstacles, marsSurfaceBuilder);
        }

        public List<Command> GetListOfCommands(string[] args)
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

        public RoverLocation DetermineStartingLocation(string[] args)
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

        private IObjective DetermineObjective(string[] args, List<Command> commands)
        {
            foreach (var argument in args)
            {
                if (argument == "map")
                {
                    return new MapSurface();
                }

                if (argument == "explore")
                {
                    return new FollowCommands(commands);
                }
                
                if (argument == "destroyer")
                {
                    return new Destroyer();
                }
            }

            return new MapSurface();
        }

        private int GetSizeOfGrid(string[] args)
        {
            return 20;
        }

        private IMarsSurfaceBuilder GetTypeOfBuilder(string[] args, int sizeOfGrid, List<Coordinate> obstacles)
        {
            foreach (var argument in args)
            {
                if (argument == "map")
                {
                    return new MappingSurfaceBuilder(sizeOfGrid);
                }
            }

            return new MarsSurfaceBuilder(obstacles);
        }
    }
}