namespace MarsRover
{
    public class ReportBuilder
    {
        public Report CreateReport(int distancedTravelled, MarsSurface initialSurface, MarsSurface finalSurface,
            RoverLocation finalLocation)
        {
            int obstaclesDiscovered = finalSurface.ObstacleCount - initialSurface.ObstacleCount;
            int obstaclesDestroyed = initialSurface.ObstacleCount - finalSurface.ObstacleCount;
            
            return new Report(distancedTravelled, obstaclesDiscovered, obstaclesDestroyed, finalSurface, finalLocation);
        }
    }
}