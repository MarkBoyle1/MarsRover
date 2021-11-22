using MarsRover.Behaviours;

namespace MarsRover
{
    public class RoverBehaviour
    {
        public ObjectLocation ExecuteCommand(ObjectLocation location, Command command, MarsSurface surface)
        {
            IBehaviour behaviour;

            if (command.Instruction is RoverInstruction.TurnLeft or RoverInstruction.TurnRight)
            {
                behaviour = new Turn(command.Instruction);
            }
            else if (command.Instruction == RoverInstruction.ShootLaser)
            {
                behaviour = new Shoot(surface);
            }
            else if (command.Instruction == RoverInstruction.LookAhead)
            {
                behaviour = new LookAhead(surface);
            }
            else
            {
                behaviour = new Move(command.Instruction);
            }

            return behaviour.ExecuteCommand(location);
        }
    }
}