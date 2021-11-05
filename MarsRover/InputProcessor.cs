using System;
using System.Collections.Generic;

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

        public List<Command> GetListOfCommands(string[] commands)
        {
            List<Command> commandList = new List<Command>();
            
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

        public RoverLocation DetermineStartingLocation(string[] startingLocation)
        {
            int xCoordinate = Convert.ToInt32(startingLocation[0]);
            int yCoordinate = Convert.ToInt32(startingLocation[1]);
            Direction directionfacing = DetermineDirection(startingLocation[2]);

            return new RoverLocation(xCoordinate, yCoordinate, directionfacing);
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
    }
}