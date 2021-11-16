using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class TestMarsSurfaceBuilder : IMarsSurfaceBuilder
    {
        private int SizeOfGrid = 20;
        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[4][];
            surface = surface.Select
                (
                    x => new string[4].Select(x => DisplaySymbol.FreeSpace).ToArray()
                )
                .ToArray();

            surface[2][3] = DisplaySymbol.Obstacle;
            return new MarsSurface(surface, 1);
        }
        
        public MarsSurface UpdateSurface(MarsSurface surface, Coordinate location, string symbol)
        {
            string[][] updatedSurface = new string[SizeOfGrid][];
            updatedSurface = updatedSurface.Select(x => new string[SizeOfGrid]).ToArray();
                
            for(int i = 0; i < SizeOfGrid; i++)
            {
                for (int j = 0; j < SizeOfGrid; j++)
                {
                    updatedSurface[i][j] = surface.GetPoint(new Coordinate(i,j));
                }
            }

            updatedSurface[location.YCoordinate][location.XCoordinate] = symbol;
            
            return new MarsSurface(updatedSurface, 1);
        }
    }
}