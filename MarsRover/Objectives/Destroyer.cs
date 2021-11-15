using System;

namespace MarsRover.Objectives
{
    public class Destroyer : IObjective
    {
        private Random random = new Random();
        public Command ReceiveCommand(MarsSurface surface, RoverLocation location)
        {
            int randomNumber = random.Next(1, 11);
            if (randomNumber >= 6)
            {
                return new Command(RoverInstruction.MoveForward);
            }
            
            if (randomNumber <= 3)
            {
                return new Command(RoverInstruction.ShootLaser);
            }
            
            if (randomNumber == 4)
            {
                return new Command(RoverInstruction.TurnLeft);
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
    }
}