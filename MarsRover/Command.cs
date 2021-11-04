using System;

namespace MarsRover
{
    public class Command
    {
        public const string TurnLeft = "l";
        public const string TurnRight = "r";
        public const string MoveForward = "f";
        public const string MoveBack = "b";
        public RoverBehaviour Instruction { get; }

        public Command(string input)
        {
            Instruction = SelectInstruction(input);
        }

        private RoverBehaviour SelectInstruction(string input)
        {
            switch (input)
            {
                case TurnLeft:
                    return RoverBehaviour.TurnLeft;
                case TurnRight:
                    return RoverBehaviour.TurnRight;
                case MoveForward:
                    return RoverBehaviour.MoveForward;
                case MoveBack:
                    return RoverBehaviour.MoveBack;
                default:
                    throw new Exception();
            }
        }

    }
}