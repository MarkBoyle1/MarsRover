using System.Collections.Generic;
using MarsRover.Behaviours;

namespace MarsRover
{
    public class RoverBehaviour
    {
        public RoverLocation ExecuteCommand(RoverLocation location, Command command)
        {
            IBehaviour behaviour;

            if (command.Instruction is RoverInstruction.TurnLeft or RoverInstruction.TurnRight)
            {
                behaviour = new Turn(command.Instruction);
            }
            else
            {
                behaviour = new Move(command.Instruction);

            }

            return behaviour.ExecuteCommand(location);
        }
    }
}