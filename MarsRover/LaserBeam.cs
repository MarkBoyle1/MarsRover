namespace MarsRover
{
    public class LaserBeam
    {
        public string Symbol { get; }
        public Coordinate Coordinate { get; }

        public LaserBeam(Coordinate coordinate, string symbol)
        {
            Symbol = symbol;
            Coordinate = coordinate;
        }
    }
}