using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class TestMarsSurfaceBuilder : IMarsSurfaceBuilder
    {
        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[4][];
            surface = surface.Select
                (
                    x => new string[4].Select(x => ".").ToArray()
                )
                .ToArray();

            surface[2][3] = "x";
            return new MarsSurface(surface);
        }

        public MarsSurface PlaceRoverOnStartingPoint(MarsSurface surface, RoverLocation startingPoint)
        {
            surface.Surface[1][1] = "^";
            
            return surface;
        }

        public MarsSurface UpdateRoverMovement(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation)
        {
            return surface;
        }
    }
}