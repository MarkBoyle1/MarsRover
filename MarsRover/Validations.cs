namespace MarsRover
{
    public class Validations
    {
        public bool LocationContainsObstacle(MarsSurface surface, RoverLocation location)
        {
            return surface.GetPoint(location.Coordinate) == "x";
        }
    }
}