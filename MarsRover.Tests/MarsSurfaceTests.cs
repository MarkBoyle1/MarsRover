using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class MarsSurfaceTests
    {
        private IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>());
        private InputProcessor _inputProcessor = new InputProcessor();
        
        [Fact]
        public void given_sizeOfGridEqualsTen_and_ObstaclesEqualsOneOne_when_CreateSurface_then_CoordinateOneOne_returns_x()
        {
            Coordinate obstacle1 = new Coordinate(1, 1);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(){obstacle1});
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
        
            string surfaceTerrainAtOneOne = marsSurface.GetPoint(obstacle1);
            
            Assert.Equal("x", surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneNorth_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingNorth()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation startingPoint = new RoverLocation(coordinate, Direction.North);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>());
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, "^");
        
            string surfaceTerrainAtOneOne = marsSurface.GetPoint(startingPoint.Coordinate);
            
            Assert.Equal("^", surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneEast_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingEast()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation startingPoint = new RoverLocation(coordinate, Direction.East);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>());
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, ">");

        
            string surfaceTerrainAtOneOne = marsSurface.GetPoint(startingPoint.Coordinate);
            
            Assert.Equal(">", surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneEast_when_MoveForward_then_CoordinateOneOne_returns_FreeSpace()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation startingPoint = new RoverLocation(coordinate, Direction.East);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>());
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, ">");
            
            RoverBehaviour _roverBehaviour = new RoverBehaviour();
        
            RoverLocation newLocation =
                _roverBehaviour.ExecuteCommand(startingPoint, new Command(RoverInstruction.MoveForward));
        
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, newLocation.Coordinate, ">");
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, ".");
            
            string surfaceTerrainAtOldLocation = marsSurface.GetPoint(startingPoint.Coordinate);
            string surfaceTerrainAtNewLocation = marsSurface.GetPoint(newLocation.Coordinate);
        
            
            Assert.Equal(".", surfaceTerrainAtOldLocation);
            Assert.Equal(">", surfaceTerrainAtNewLocation);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneEast_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingE()
        {
            string[] obstacles = new[] {"obstacles:1,1;1,2;0,3"};
            List<Coordinate> obstacleCoordinates = _inputProcessor.TurnObstacleInputsIntoCoordinates(obstacles);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(obstacleCoordinates);
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
        
            string obstacle1 = marsSurface.GetPoint(obstacleCoordinates[0]);
            string obstacle2 = marsSurface.GetPoint(obstacleCoordinates[1]);
            string obstacle3 = marsSurface.GetPoint(obstacleCoordinates[2]);
        
            
            Assert.Equal("x", obstacle1);
            Assert.Equal("x", obstacle2);
            Assert.Equal("x", obstacle3);
        }
        
        [Fact]
        public void CreateSurfaceForMapping()
        {
            IMarsSurfaceBuilder _mappingBuilder = new MappingSurfaceBuilder(20);
            RoverLocation startingPoint = new RoverLocation(new Coordinate(1, 1), Direction.North);
        
            MarsSurface surface = _mappingBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, startingPoint.Coordinate, "^");
            
            Assert.Equal(" ", surface.GetPoint(new Coordinate(1,2 )));
        }
    }
}