using System.Collections.Generic;

namespace MarsRover
{
    public interface IMarsSurfaceBuilder
    {
        MarsSurface CreateSurface();
        MarsSurface PlaceRoverOnStartingPoint(MarsSurface surface, RoverLocation startingPoint);
        MarsSurface UpdateRoverMovement(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation);

    }
}