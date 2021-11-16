namespace MarsRover
{
    public class MarsSurface
    {
        public string[][] Surface { get; }
        public int ObstacleCount { get; }
        public MarsSurface(string[][] surface, int obstacleCount)
        {
            Surface = surface;
            ObstacleCount = obstacleCount;
        }

        public string GetPoint(Coordinate coordinate)
        {
            return Surface[coordinate.YCoordinate][coordinate.XCoordinate];
        }
    }
}