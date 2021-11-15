namespace MarsRover.Objectives
{
    public interface IObjective
    {
        Command ReceiveCommand(MarsSurface surface, RoverLocation location);
        Command ReceiveCommandForObstacle();
    }
}