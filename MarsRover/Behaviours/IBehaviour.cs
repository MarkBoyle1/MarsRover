namespace MarsRover.Behaviours
{
    public interface IBehaviour
    {
        ObjectLocation ExecuteCommand(ObjectLocation location);
    }
}