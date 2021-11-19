namespace MarsRover.Objectives
{
    public interface IObjective
    {
        Command ReceiveCommand();
        Command ReceiveCommandForObstacle();
        bool CheckForCompletion(MarsSurface surface);
    }
}