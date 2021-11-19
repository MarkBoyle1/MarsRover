using System;

namespace MarsRover.Objectives
{
    public class MapSurface : IObjective
    {
        private Random random = new Random();
        public Command ReceiveCommand()
        {
            if (random.Next(1, 11) > 1)
            {
                return new Command(RoverInstruction.MoveForward);
            }
            
            return new Command(RoverInstruction.TurnRight);
        }
        
        public Command ReceiveCommandForObstacle()
        {
            if (random.Next(1, 11) > 5)
            {
                return new Command(RoverInstruction.TurnLeft);
            }
            
            return new Command(RoverInstruction.TurnRight);
        }
        
        public bool CheckForCompletion(MarsSurface surface)
        {
            return surface.AreasDiscovered == 1;
        }
    }
}