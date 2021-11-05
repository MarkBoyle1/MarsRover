using System.Collections.Generic;

namespace MarsRover
{
    public class RoverBehaviour
    {
        private int sizeOfGrid = 10;
        public RoverLocation ExecuteCommand(RoverLocation location, Command command)
        {
            int xCoordinate = location.Coordinate.XCoordinate;
            int yCoordinate = location.Coordinate.YCoordinate;
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
            int newCoordinate = coordinate;

            if (currentDirection is Direction.North or Direction.South)
            {
                return newCoordinate;
            }

            if (instruction == RoverInstruction.MoveForward)
            {
                newCoordinate = currentDirection == Direction.East ? coordinate + 1 : coordinate - 1;
                return WrapAroundPlanetIfRequired(newCoordinate, sizeOfGrid);
            }
            
            newCoordinate = currentDirection == Direction.East ? coordinate - 1 : coordinate + 1;
            return WrapAroundPlanetIfRequired(newCoordinate, sizeOfGrid);
        }
        
        private int MoveAlongYCoordinate(int coordinate, Direction currentDirection, RoverInstruction instruction)
        {
            int newCoordinate = coordinate;
            
            if (currentDirection is Direction.East or Direction.West)
            {
                return newCoordinate;
            }

            if (instruction == RoverInstruction.MoveForward)
            {
                newCoordinate = currentDirection == Direction.North ? coordinate - 1 : coordinate + 1;
                return WrapAroundPlanetIfRequired(newCoordinate, sizeOfGrid);
            }
            
            newCoordinate = currentDirection == Direction.North ? coordinate + 1 : coordinate - 1;
            return WrapAroundPlanetIfRequired(newCoordinate, sizeOfGrid);
        }

        private int WrapAroundPlanetIfRequired(int coordinate, int sizeOfGrid)
        {
            if (coordinate < 0)
            {
                return sizeOfGrid - 1;
            }

            if (coordinate > sizeOfGrid - 1)
            {
                return 0;
            }

            return coordinate;
        }
    }
}