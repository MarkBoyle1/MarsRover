using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class MappingSurfaceBuilder : IMarsSurfaceBuilder
    {
        private int SizeOfGrid;
        public MappingSurfaceBuilder(int sizeOfGrid)
        {
            SizeOfGrid = sizeOfGrid;
        }
        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[SizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[SizeOfGrid].Select(x => DisplaySymbol.UnknownSpace).ToArray()
                )
                .ToArray();

            return new MarsSurface(surface);
        }
        
        public MarsSurface UpdateSurface(MarsSurface surface, Coordinate location, string symbol)
        {
            string[][] updatedSurface = new string[SizeOfGrid][];
            updatedSurface = updatedSurface.Select(x => new string[SizeOfGrid]).ToArray();
                
            for(int x = 0; x < SizeOfGrid; x++)
            {
                for (int y = 0; y < SizeOfGrid; y++)
                {
                    updatedSurface[y][x] = surface.GetPoint(new Coordinate(x,y));
                }
            }

            updatedSurface[location.YCoordinate][location.XCoordinate] = symbol;
            
            return new MarsSurface(updatedSurface);
        }
    }
}