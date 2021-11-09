namespace MarsRover
{
    public class RoverLocation
    {
        public Coordinate Coordinate { get; }
        public Direction DirectionFacing { get; }

        public RoverLocation(int xCoordinate, int yCoordinate, Direction directionFacing)
        {
            Coordinate = new Coordinate(xCoordinate, yCoordinate);
            DirectionFacing = directionFacing;
        }
    }
}