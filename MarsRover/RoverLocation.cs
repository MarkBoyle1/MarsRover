
namespace MarsRover
{
    public class RoverLocation
    {
        public Coordinate Coordinate { get; }
        public Direction DirectionFacing { get; }

        public RoverLocation(Coordinate coordinate, Direction directionFacing)
        {
            Coordinate = coordinate;
            DirectionFacing = directionFacing;
        }
    }
}