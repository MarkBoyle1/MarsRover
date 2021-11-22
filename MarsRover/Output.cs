using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MarsRover
{
    public class Output : IOutput
    {
        private ConsoleColor _explosionColour = ConsoleColor.Yellow;
        
        public void DisplaySurface(MarsSurface surface, int threadSpeed)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            for(int row = 0; row < surface.SizeOfGrid; row++)
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
            Thread.Sleep(200);
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

        public void CreateReportFile(Report report)
        {
            List<string> reportData = new List<string>()
            {
                "Mission Completed at:" + DateTime.Now,
                "DistanceTravelled:" + report.DistanceTravelled,
            };

            if (report.ObstaclesDestroyed > 0)
            {
                reportData.Add("ObstaclesDestroyed:" + report.ObstaclesDestroyed);
            }
            
            if (report.ObstaclesDiscovered > 0)
            {
                reportData.Add("ObstaclesDiscovered:" + report.ObstaclesDiscovered);
            }

            string filePath = @"/Users/Mark.Boyle/Desktop/c#/katas/MarsRover/MarsRover/MissionReport.csv";
            File.WriteAllLinesAsync(filePath, reportData);
        }
    }
}