namespace MarsRover
{
    public interface IOutput
    {
        void DisplaySurface(MarsSurface surface, int threadSpeed);
        void DisplayMessage(string message);
        void DisplayReport(Report report);
        void CreateReportFile(Report report);
    }
}