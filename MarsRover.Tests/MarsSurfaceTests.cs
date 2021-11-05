using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class MarsSurfaceTests
    {
        private MarsSurfaceFactory _marsSurfaceFactory = new MarsSurfaceFactory();
        
        [Fact]
        public void given_sizeOfGridEqualsTen_and_ObstaclesEqualsOneOne_when_CreateSurface_then_CoordinateOneOne_returns_x()
        {
            Coordinate obstacle1 = new Coordinate(1, 1);
            MarsSurface marsSurface = _marsSurfaceFactory.CreateSurface(10, new List<Coordinate>(){obstacle1});

            string surfaceTerrainAtOneOne = marsSurface.GetPoint(obstacle1);
            
            Assert.Equal("x", surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneNorth_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingNorth()
        {
            RoverLocation startingPoint = new RoverLocation(1, 1, Direction.North);
            MarsSurface marsSurface = _marsSurfaceFactory.CreateSurface(10, new List<Coordinate>());
            marsSurface = _marsSurfaceFactory.PlaceRoverOnStartingPoint(marsSurface, startingPoint);

            string surfaceTerrainAtOneOne = marsSurface.GetPoint(startingPoint.Coordinate);
            
            Assert.Equal("^", surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneEast_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingEast()
        {
            RoverLocation startingPoint = new RoverLocation(1, 1, Direction.East);
            MarsSurface marsSurface = _marsSurfaceFactory.CreateSurface(10, new List<Coordinate>());
            marsSurface = _marsSurfaceFactory.PlaceRoverOnStartingPoint(marsSurface, startingPoint);

            string surfaceTerrainAtOneOne = marsSurface.GetPoint(startingPoint.Coordinate);
            
            Assert.Equal(">", surfaceTerrainAtOneOne);
        }
    }
}