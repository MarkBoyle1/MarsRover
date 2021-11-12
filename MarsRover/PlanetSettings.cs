using System.Collections.Generic;

namespace MarsRover
{
    public class PlanetSettings
    {
        public int SizeOfGrid { get; }
        public List<Coordinate> Obstacles { get; }
        public IMarsSurfaceBuilder MarsSurfaceBuilder { get; }

        public PlanetSettings(int sizeOfGrid, List<Coordinate> obstacles, IMarsSurfaceBuilder marsSurfaceBuilder)
        {
            SizeOfGrid = sizeOfGrid;
            Obstacles = obstacles;
            MarsSurfaceBuilder = marsSurfaceBuilder;
        }
    }
}