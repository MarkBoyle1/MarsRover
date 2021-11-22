namespace MarsRover
{
    public class MarsSurface
    {
        public string[][] Surface { get; }
        public int ObstacleCount { get; }
        public int AreasDiscovered { get; }
        public int SizeOfGrid { get; }
        public MarsSurface(string[][] surface, int obstacleCount, int areasDiscovered)
        {
            Surface = surface;
            ObstacleCount = obstacleCount;
            AreasDiscovered = areasDiscovered;
            SizeOfGrid = surface[0].Length;
        }

        public string GetPoint(Coordinate coordinate)
        {
            return Surface[coordinate.YCoordinate][coordinate.XCoordinate];
        }
    }
}