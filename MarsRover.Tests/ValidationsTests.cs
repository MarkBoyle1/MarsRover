using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ValidationsTests
    {
        private Validations _validations = new Validations();
        private MarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder();
        
        [Fact]
        public void given_obstacleAtOneTwo_and_RoverLocationAtOneTwo_when_LocationContainsObstacle_then_return_true()
        {
            Coordinate coordinate = new Coordinate(1, 2);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.East);
            Coordinate obstacle1 = new Coordinate(1, 2);
            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface(new List<Coordinate>() {obstacle1});

            bool isObstacle = _validations.LocationContainsObstacle(marsSurface, roverLocation);
            
            Assert.True(isObstacle);
        }
    }
}