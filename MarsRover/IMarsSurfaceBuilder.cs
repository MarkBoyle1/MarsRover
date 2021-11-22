
namespace MarsRover
{
    public interface IMarsSurfaceBuilder
    {
        MarsSurface CreateSurface();
        MarsSurface UpdateSurface(MarsSurface surface, Coordinate location, string symbol);
    }
}