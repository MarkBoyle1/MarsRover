using MarsRover.Behaviours;

namespace MarsRover
{
    public class RoverBehaviour
    {
        private UtilityMethods _utility;
        public RoverBehaviour(UtilityMethods utility)
        {
            _utility = utility;
        }
        public RoverLocation ExecuteCommand(RoverLocation location, Command command)
        {
            IBehaviour behaviour;

            if (command.Instruction is RoverInstruction.TurnLeft or RoverInstruction.TurnRight)
            {
                behaviour = new Turn(command.Instruction);
            }
            else
            {
                behaviour = new Move(command.Instruction, _utility);

            }

            return behaviour.ExecuteCommand(location);
        }
    }
}