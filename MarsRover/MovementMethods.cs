using MarsRover.Exceptions;

namespace MarsRover
{
    public class MovementMethods
    {
        private int _sizeOfGrid;
        public MovementMethods(int sizeOfGrid)
        {
            _sizeOfGrid = sizeOfGrid;
        }
        public Coordinate GetNextSpace(Coordinate coordinate, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Coordinate(coordinate.XCoordinate, coordinate.YCoordinate - 1);
                case Direction.East:
                    return new Coordinate(coordinate.XCoordinate + 1, coordinate.YCoordinate);
                case Direction.South:
                    return new Coordinate(coordinate.XCoordinate, coordinate.YCoordinate + 1);
                case Direction.West:
                    return new Coordinate(coordinate.XCoordinate - 1, coordinate.YCoordinate);
                default:
                    throw new InvalidDirectionException(direction.ToString());
            }
        }
        
        public bool LocationIsOnGrid(int sizeOfGrid, Coordinate coordinate)
        {
            if (coordinate.XCoordinate >= sizeOfGrid || coordinate.XCoordinate < 0)
            {
                return false;
            }
            
            if (coordinate.YCoordinate >= sizeOfGrid || coordinate.YCoordinate < 0)
            {
                return false;
            }

            return true;
        }
        
        public Coordinate WrapAroundPlanetIfRequired(Coordinate coordinate)
        {
            int xCoordinate = AdjustIndividualCoordinate(coordinate.XCoordinate);
            int yCoordinate = AdjustIndividualCoordinate(coordinate.YCoordinate);
        
            return new Coordinate(xCoordinate, yCoordinate);
        }
        
        private int AdjustIndividualCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                return _sizeOfGrid - 1;
            }
            
            if (coordinate > _sizeOfGrid - 1)
            {
                return 0;
            }
            
            return coordinate;
        }
    }
}