namespace MarsRover
{
    public class Validations
    {
        public bool LocationContainsObstacle(MarsSurface surface, RoverLocation location)
        {
            return surface.GetPoint(location.Coordinate) == DisplaySymbol.Obstacle;
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
    }
}