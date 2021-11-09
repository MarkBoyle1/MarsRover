using System;
using System.Linq;
using System.Threading;

namespace MarsRover
{
    public class Output
    {
        public void DisplaySurface(MarsSurface surface)
        {
            Thread.Sleep(1000);
            Console.Clear();
            // Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            // Console.Beep();
            
            for(int row = 0; row < 10; row++)
            {
                
                // Console.WriteLine(String.Join(',', surface.Surface[row]).Replace(',', ' '));

                foreach (var point in surface.Surface[row])
                {
                    if (point == "x")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(point == ".")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(point + " ");
                }
                Console.WriteLine();
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}