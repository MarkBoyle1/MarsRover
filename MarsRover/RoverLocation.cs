namespace MarsRover
{
    public class RoverLocation
    {
        // public int XCoordinate { get; }
        // public int YCoordinate { get; }
        public Coordinate Coordinate { get; }
        public Direction DirectionFacing { get; }

        public RoverLocation(int xCoordinate, int yCoordinate, Direction directionFacing)
        {
            // XCoordinate = xCoordinate;
            // YCoordinate = yCoordinate;
            Coordinate = new Coordinate(xCoordinate, yCoordinate);
            DirectionFacing = directionFacing;
        }
    }
}