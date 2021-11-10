using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ValidationsTests
    {
        private Validations _validations = new Validations();

        [Fact]
        public void given_obstacleAtOneTwo_and_RoverLocationAtOneTwo_when_LocationContainsObstacle_then_return_true()
        {
            Coordinate coordinate = new Coordinate(1, 2);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.East);
            Coordinate obstacle1 = new Coordinate(1, 2);
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(){obstacle1});

            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();

            bool isObstacle = _validations.LocationContainsObstacle(marsSurface, roverLocation);
            
            Assert.True(isObstacle);
        }
    }
}