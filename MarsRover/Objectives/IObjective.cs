namespace MarsRover.Objectives
{
    public interface IObjective
    {
        Command ReceiveCommand();
        Command ReceiveCommandForObstacle();
        bool CheckForCompletion(Report report);
    }
}