namespace MarsRover
{
    public class Report
    {
        public int ObstaclesDiscovered { get; }
        public int ObstaclesDestroyed { get; }
        public int DistanceTravelled { get; }
        public MarsSurface FinalSurface { get; }
        public RoverLocation FinalLocation { get; }

        public Report(int distanceTravelled, int obstaclesDiscovered, int obstaclesDestroyed, MarsSurface finalSurface,
            RoverLocation finalLocation)
        {
            DistanceTravelled = distanceTravelled;
            ObstaclesDiscovered = obstaclesDiscovered;
            ObstaclesDestroyed = obstaclesDestroyed;
            FinalSurface = finalSurface;
            FinalLocation = finalLocation;
        }
    }
}