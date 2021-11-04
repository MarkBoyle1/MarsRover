using System.Collections.Generic;

namespace MarsRover
{
    public class RoverBehaviour
    {
        public RoverLocation ExecuteCommand(RoverLocation location, Command command)
        {
            int xCoordinate = location.XCoordinate;
            int yCoordinate = location.YCoordinate;
            Direction directionFacing = location.DirectionFacing;
            
            switch (command.Instruction)
            {
                case RoverInstruction.TurnLeft:
                    directionFacing = directionFacing == Direction.North ? Direction.West : directionFacing - 1;
                    break;
                case RoverInstruction.TurnRight:
                    directionFacing = directionFacing == Direction.West ? Direction.North : directionFacing + 1;
                    break;
                case RoverInstruction.MoveForward:
                    xCoordinate = MoveAlongXCoordinate(xCoordinate, directionFacing, RoverInstruction.MoveForward);
                    yCoordinate = MoveAlongYCoordinate(yCoordinate, directionFacing, RoverInstruction.MoveForward);
                    break;
                case RoverInstruction.MoveBack:
                    xCoordinate = MoveAlongXCoordinate(xCoordinate, directionFacing, RoverInstruction.MoveBack);
                    yCoordinate = MoveAlongYCoordinate(yCoordinate, directionFacing, RoverInstruction.MoveBack);
                    break;
            }

            return new RoverLocation(xCoordinate, yCoordinate, directionFacing);
        }

        private int MoveAlongXCoordinate(int coordinate, Direction currentDirection, RoverInstruction instruction)
        {
            if (currentDirection == Direction.North || currentDirection == Direction.South)
            {
                return coordinate;
            }

            if (instruction == RoverInstruction.MoveForward)
            {
                return currentDirection == Direction.East ? coordinate++ : coordinate--;
            }
            
            return currentDirection == Direction.East ? coordinate-- : coordinate++;
        }
        
        private int MoveAlongYCoordinate(int coordinate, Direction currentDirection, RoverInstruction instruction)
        {
            if (currentDirection == Direction.East || currentDirection == Direction.West)
            {
                return coordinate;
            }

            if (instruction == RoverInstruction.MoveForward)
            {
                return currentDirection == Direction.North ? coordinate + 1 : coordinate - 1;
            }
            
            return currentDirection == Direction.North ? coordinate - 1 : coordinate + 1;
        }
    }
}