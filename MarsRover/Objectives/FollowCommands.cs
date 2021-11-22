using System.Collections.Generic;

namespace MarsRover.Objectives
{
    public class FollowCommands : IObjective
    {
        private List<Command> _commands;
        
        public FollowCommands(List<Command> commands)
        {
            _commands = commands;
        }
        
        public Command ReceiveCommand()
        {
            if (_commands.Count == 0)
            {
                return new Command(RoverInstruction.Stop);
            }
            
            Command command = _commands[0];
            _commands.RemoveAt(0);
            return command;
        }

        public Command ReceiveCommandForObstacle()
        {
            return new Command(RoverInstruction.Stop);
        }
        
        public bool CheckForCompletion(Report report)
        {
            return false;
        }
    }
}