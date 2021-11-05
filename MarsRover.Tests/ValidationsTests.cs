using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ValidationsTests
    {
        private Validations _validations = new Validations();
        private MarsSurfaceFactory _marsSurfaceFactory = new MarsSurfaceFactory();
        
        [Fact]
        public void given_obstacleAtOneTwo_and_RoverLocationAtOneTwo_when_LocationContainsObstacle_then_return_true()
        {
            RoverLocation roverLocation = new RoverLocation(1, 2, Direction.East);
            Coordinate obstacle1 = new Coordinate(1, 2);
            MarsSurface marsSurface = _marsSurfaceFactory.CreateSurface(10, new List<Coordinate>() {obstacle1});

            bool isObstacle = _validations.LocationContainsObstacle(marsSurface, roverLocation);
            
            Assert.True(isObstacle);
        }
    }
}