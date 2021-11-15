using System.Collections.Generic;

namespace MarsRover
{
    public interface IMarsSurfaceBuilder
    {
        MarsSurface CreateSurface();
        // MarsSurface PlaceRoverOnStartingPoint(MarsSurface surface, RoverLocation startingPoint);
        // MarsSurface UpdateRoverMovement(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation);
        //
        // MarsSurface UpdateLaserShot(MarsSurface surface, Coordinate oldLocation, Coordinate newLocation,
        //     string displaySymbol);

        MarsSurface UpdateSurface(MarsSurface surface, Coordinate location, string symbol);
    }
}