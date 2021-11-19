namespace MarsRover
{
    public class Report
    {
        public int ObstaclesDiscovered { get; }
        public int ObstaclesDestroyed { get; }
        public int DistanceTravelled { get; }
        public MarsSurface CurrentSurface { get; }
        public RoverLocation FinalLocation { get; }

        public Report(int distanceTravelled, int obstaclesDiscovered, int obstaclesDestroyed, MarsSurface currentSurface,
            RoverLocation finalLocation)
        {
            DistanceTravelled = distanceTravelled;
            ObstaclesDiscovered = obstaclesDiscovered;
            ObstaclesDestroyed = obstaclesDestroyed;
            CurrentSurface = currentSurface;
            FinalLocation = finalLocation;
        }
    }
}