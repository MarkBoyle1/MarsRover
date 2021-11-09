namespace MarsRover
{
    public class MarsSurface
    {
        public string[][] Surface { get; }
        public MarsSurface(string[][] surface)
        {
            Surface = surface;
        }

        public string GetPoint(Coordinate coordinate)
        {
            return Surface[coordinate.YCoordinate][coordinate.XCoordinate];
        }
    }
}