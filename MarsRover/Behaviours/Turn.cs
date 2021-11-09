namespace MarsRover.Behaviours
{
    public class Turn : IBehaviour
    {
        private RoverInstruction _turnDirection;
        
        public Turn(RoverInstruction turnDirection)
        {
            _turnDirection = turnDirection;
        }
        
        public RoverLocation ExecuteCommand(RoverLocation location)
        {
            int xCoordinate = location.Coordinate.XCoordinate;
            int yCoordinate = location.Coordinate.YCoordinate;
            Direction directionFacing = location.DirectionFacing;

            if (_turnDirection == RoverInstruction.TurnLeft)
            {
                directionFacing = directionFacing == Direction.North ? Direction.West : directionFacing - 1;
            }

            if (_turnDirection == RoverInstruction.TurnRight)
            {
                directionFacing = directionFacing == Direction.West ? Direction.North : directionFacing + 1;
            }

            return new RoverLocation(xCoordinate, yCoordinate, directionFacing);
        }
    }
}