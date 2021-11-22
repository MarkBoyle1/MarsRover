using System.Linq;

namespace MarsRover
{
    public class TestBlankSurfaceBuilder : IMarsSurfaceBuilder
    {
        private int SizeOfGrid = 4;
        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[SizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[SizeOfGrid].Select(x => DisplaySymbol.FreeSpace).ToArray()
                )
                .ToArray();
            
            return new MarsSurface(surface, 0, SizeOfGrid * SizeOfGrid);
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
            
            return new MarsSurface(updatedSurface, 0, SizeOfGrid * SizeOfGrid);
        }
    }
}