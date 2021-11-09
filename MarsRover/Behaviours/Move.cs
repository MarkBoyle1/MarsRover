namespace MarsRover.Behaviours
{
    public class Move : IBehaviour
    {
        private int sizeOfGrid = 10;
        private RoverInstruction _instruction;

        public Move(RoverInstruction instruction)
        {
            _instruction = instruction;
        }

        public RoverLocation ExecuteCommand(RoverLocation location)
        {
            Coordinate newCoordinate = location.Coordinate;
            Direction directionFacing = location.DirectionFacing;

            if (_instruction == RoverInstruction.MoveForward)
            {
                newCoordinate = MoveRover(location.Coordinate, directionFacing, RoverInstruction.MoveForward);
            }

            if (_instruction == RoverInstruction.MoveBack)
            {
                newCoordinate = MoveRover(location.Coordinate, directionFacing, RoverInstruction.MoveBack);
            }
            
            return new RoverLocation(newCoordinate.XCoordinate, newCoordinate.YCoordinate, directionFacing);
        }
        
        private Coordinate MoveRover(Coordinate coordinate, Direction currentDirection, RoverInstruction instruction)
        {
            int xCoordinate = coordinate.XCoordinate;
            int yCoordinate = coordinate.YCoordinate;

            switch (currentDirection)
            {
                case Direction.North:
                    yCoordinate = instruction == RoverInstruction.MoveForward ? yCoordinate - 1 : yCoordinate + 1;
                    break;
                case Direction.East:
                    xCoordinate = instruction == RoverInstruction.MoveForward ? xCoordinate + 1 : xCoordinate - 1;
                    break;
                case Direction.South:
                    yCoordinate = instruction == RoverInstruction.MoveForward ? yCoordinate + 1 : yCoordinate - 1;
                    break;
                case Direction.West:
                    xCoordinate = instruction == RoverInstruction.MoveForward ? xCoordinate - 1 : xCoordinate + 1;
                    break;
            }

            xCoordinate = WrapAroundPlanetIfRequired(xCoordinate);
            yCoordinate = WrapAroundPlanetIfRequired(yCoordinate);

            Coordinate newCoordinate = new Coordinate(xCoordinate, yCoordinate);

            return newCoordinate;
        }
        
        private int WrapAroundPlanetIfRequired(int coordinate)
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