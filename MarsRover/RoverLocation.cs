
using MarsRover.Exceptions;

namespace MarsRover
{
    public class RoverLocation
    {
        public Coordinate Coordinate { get; }
        public Direction DirectionFacing { get; }
        public string Symbol { get; }

        public RoverLocation(Coordinate coordinate, Direction directionFacing, string symbol)
        {
            Coordinate = coordinate;
            DirectionFacing = directionFacing;
            Symbol = symbol;
        }
        public RoverLocation(Coordinate coordinate, Direction directionFacing)
        {
            Coordinate = coordinate;
            DirectionFacing = directionFacing;
            Symbol = DetermineDirectionOfRover(directionFacing);
        }
        
        public string DetermineDirectionOfRover(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return DisplaySymbol.RoverNorthFacing;
                case Direction.East:
                    return DisplaySymbol.RoverEastFacing;
                case Direction.South:
                    return DisplaySymbol.RoverSouthFacing;
                case Direction.West:
                    return DisplaySymbol.RoverWestFacing;
                default:
                    throw new InvalidDirectionException(direction.ToString());
            }
        }
    }
}