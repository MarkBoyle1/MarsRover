namespace MarsRover
{
    public class InputProcessor
    {
        public Command TurnInputIntoCommand(string input)
        {
            return new Command(input);
        }
    }
}