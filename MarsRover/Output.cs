using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace MarsRover
{
    public class Output
    {
        private ConsoleColor _explosionColour = ConsoleColor.Yellow;
        public void DisplaySurface(MarsSurface surface, int threadSpeed)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            // Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            // Console.Beep();
            
            for(int row = 0; row < 20; row++)
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
            Console.Clear();

        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        private ConsoleColor SwapExplosionColour(ConsoleColor currentColour)
        {
            return currentColour == ConsoleColor.Yellow ? ConsoleColor.Red : ConsoleColor.Yellow;
        }
    }
}