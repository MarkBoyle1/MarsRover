using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ReportTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        private ReportBuilder _reportBuilder = new ReportBuilder();
        
        [Fact]
        public void CountNumberOfObstaclesOnSurface()
        {
            string[] obstacles = new[] {"obstacles:1,1;1,2;0,3"};
            List<Coordinate> obstacleCoordinates = _inputProcessor.TurnObstacleInputsIntoCoordinates(obstacles);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(obstacleCoordinates);
        
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            
            Assert.Equal(3, surface.ObstacleCount);
        }
        
        [Fact]
        public void ReportOnObstaclesDiscovered()
        {
            IMarsSurfaceBuilder _marsSurfaceBuilderFirst = new MarsSurfaceBuilder(new List<Coordinate>() { new Coordinate(1,1)});
            MarsSurface _initialSurface = _marsSurfaceBuilderFirst.CreateSurface();
            
            List<Coordinate> obstacles = new List<Coordinate>() {new Coordinate(3, 3), new Coordinate(2,2)};
            IMarsSurfaceBuilder _marsSurfaceBuilderSecond = new MarsSurfaceBuilder(obstacles);
            MarsSurface _finalSurface = _marsSurfaceBuilderSecond.CreateSurface();

            int distanceTravelled = 10;
            RoverLocation finalLocation = new RoverLocation(new Coordinate(1, 1), Direction.South);

            Report report =
                _reportBuilder.CreateReport(distanceTravelled, _initialSurface, _finalSurface, finalLocation);
            
            Assert.Equal(1, report.ObstaclesDiscovered);
        }
    }
}