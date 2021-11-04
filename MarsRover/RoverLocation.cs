namespace MarsRover
{
    public class RoverLocation
    {
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public Direction DirectionFacing { get; }

        public RoverLocation(int xCoordinate, int yCoordinate, Direction directionFacing)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            DirectionFacing = directionFacing;
        }
    }
}