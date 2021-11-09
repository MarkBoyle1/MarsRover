namespace MarsRover.Behaviours
{
    public interface IBehaviour
    {
        RoverLocation ExecuteCommand(RoverLocation location);
    }
}