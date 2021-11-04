using System;

namespace MarsRover
{
    public class Command
    {
        public RoverInstruction Instruction { get; }

        public Command(RoverInstruction input)
        {
            Instruction = input;
        }

        

    }
}