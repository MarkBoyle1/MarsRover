using System;

namespace MarsRover
{
    public class InputProcessor
    {
        public const string TurnLeft = "l";
        public const string TurnRight = "r";
        public const string MoveForward = "f";
        public const string MoveBack = "b";
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
    }
}