using System;
using System.Text;
using System.Threading;

namespace MarsRover
{
    public class Output
    {
        private ConsoleColor _explosionColour = ConsoleColor.Yellow;
        private int _sizeOfGrid;
        public Output(int sizeOfGrid)
        {
            _sizeOfGrid = sizeOfGrid;
        }
        public void DisplaySurface(MarsSurface surface, int threadSpeed)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            for(int row = 0; row < _sizeOfGrid; row++)
            {
                foreach (var point in surface.Surface[row])
                {
                    if (point == DisplaySymbol.Obstacle)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(point == DisplaySymbol.FreeSpace)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (point is DisplaySymbol.LaserVertical or DisplaySymbol.LaserHorizontal)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                    }
                    else if(point == DisplaySymbol.Explosion)
                    {
                        _explosionColour = SwapExplosionColour(_explosionColour);
                        Console.ForegroundColor = _explosionColour;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(point + " ");
                }
                Console.WriteLine();
            }
            Thread.Sleep(threadSpeed);
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        private ConsoleColor SwapExplosionColour(ConsoleColor currentColour)
        {
            return currentColour == ConsoleColor.Yellow ? ConsoleColor.Red : ConsoleColor.Yellow;
        }

        public void DisplayReport(Report report)
        {
            Console.WriteLine(OutputMessages.MissionComplete);
            Console.WriteLine(OutputMessages.DistanceTravelled + report.DistanceTravelled);
            if (report.ObstaclesDiscovered > 0)
            {
                Console.WriteLine(OutputMessages.ObstaclesDiscovered + report.ObstaclesDiscovered);
            }

            if (report.ObstaclesDestroyed > 0)
            {
                Console.WriteLine(OutputMessages.ObstaclesDestroyed + report.ObstaclesDestroyed);
            }
        }
    }
}