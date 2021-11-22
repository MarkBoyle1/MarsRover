using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class MarsSurfaceTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        
        [Fact]
        public void given_ObstaclesEqualsOneOne_when_CreateSurface_then_CoordinateOneOne_returns_Obstacle()
        {
            Coordinate obstacle1 = new Coordinate(1, 1);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(){obstacle1}, 20);
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
        
            string surfaceTerrainAtOneOne = marsSurface.GetPoint(obstacle1);
            
            Assert.Equal(DisplaySymbol.Obstacle, surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneNorth_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingNorth()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation startingPoint = new ObjectLocation(coordinate, Direction.North, DisplaySymbol.RoverNorthFacing);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(), 20);
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, DisplaySymbol.RoverNorthFacing);
        
            string surfaceTerrainAtOneOne = marsSurface.GetPoint(startingPoint.Coordinate);
            
            Assert.Equal(DisplaySymbol.RoverNorthFacing, surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneEast_when_PlaceRoverOnStartingPosition_then_CoordinateOneOne_returns_RoverFacingEast()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation startingPoint = new ObjectLocation(coordinate, Direction.East, DisplaySymbol.RoverEastFacing);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(), 20);
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, DisplaySymbol.RoverEastFacing);

        
            string surfaceTerrainAtOneOne = marsSurface.GetPoint(startingPoint.Coordinate);
            
            Assert.Equal(DisplaySymbol.RoverEastFacing, surfaceTerrainAtOneOne);
        }
        
        [Fact]
        public void given_startingLocationEqualsOneOneEast_when_MoveForward_then_CoordinateOneOne_returns_FreeSpace()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation startingPoint = new ObjectLocation(coordinate, Direction.East, DisplaySymbol.RoverEastFacing);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(), 20);
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, DisplaySymbol.RoverEastFacing);
            
            RoverBehaviour _roverBehaviour = new RoverBehaviour();
        
            ObjectLocation newLocation =
                _roverBehaviour.ExecuteCommand(startingPoint, new Command(RoverInstruction.MoveForward), marsSurface);
        
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, newLocation.Coordinate, DisplaySymbol.RoverEastFacing);
            marsSurface = _marsSurfaceBuilder.UpdateSurface(marsSurface, startingPoint.Coordinate, DisplaySymbol.FreeSpace);
            
            string surfaceTerrainAtOldLocation = marsSurface.GetPoint(startingPoint.Coordinate);
            string surfaceTerrainAtNewLocation = marsSurface.GetPoint(newLocation.Coordinate);
        
            
            Assert.Equal(DisplaySymbol.FreeSpace, surfaceTerrainAtOldLocation);
            Assert.Equal(DisplaySymbol.RoverEastFacing, surfaceTerrainAtNewLocation);
        }
        
        [Fact]
        public void given_obstaclesContainsOneOne_when_CreateSurface_then_CoordinateOneOneContainsObstacle()
        {
            string[] obstacles = new[] {"obstacles:1,1"};
            List<Coordinate> obstacleCoordinates = _inputProcessor.TurnObstacleInputsIntoCoordinates(obstacles);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(obstacleCoordinates, 20);
        
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();
        
            string obstacle1 = marsSurface.GetPoint(obstacleCoordinates[0]);
            
            Assert.Equal(DisplaySymbol.Obstacle, obstacle1);
        }
        
        [Fact]
        public void given_IMarsSurfaceBuilderEqualsMappingSurfaceBuilder_when_CreateSurface_then_AreasDiscoveredEqualsZero()
        {
            IMarsSurfaceBuilder _mappingBuilder = new MappingSurfaceBuilder(20);

            MarsSurface surface = _mappingBuilder.CreateSurface();
            
            Assert.Equal(0, surface.AreasDiscovered);
        }
    }
}