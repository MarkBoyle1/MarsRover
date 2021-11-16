using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ReportTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        
        [Fact]
        public void CountNumberOfObstaclesOnSurface()
        {
            string[] obstacles = new[] {"obstacles:1,1;1,2;0,3"};
            List<Coordinate> obstacleCoordinates = _inputProcessor.TurnObstacleInputsIntoCoordinates(obstacles);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(obstacleCoordinates);
        
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            
            Assert.Equal(3, surface.ObstacleCount);
        }
    }
}