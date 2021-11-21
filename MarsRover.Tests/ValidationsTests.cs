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
            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(new List<Coordinate>(){obstacle1}, 20);

            MarsSurface marsSurface = _marsSurfaceBuilder.CreateSurface();

            bool isObstacle = _validations.LocationContainsObstacle(marsSurface, roverLocation);
            
            Assert.True(isObstacle);
        }

        [Fact]
        public void given_xcoordinateEqualsMinusOne_when_LocationIsOnGrid_then_return_False()
        {
            Coordinate coordinate = new Coordinate(-1, 0);
            
            Assert.False(_validations.LocationIsOnGrid(20, coordinate));
        }
        
        [Fact]
        public void given_ycoordinateEqualsMinusOne_when_LocationIsOnGrid_then_return_False()
        {
            Coordinate coordinate = new Coordinate(0, -1);
            
            Assert.False(_validations.LocationIsOnGrid(20, coordinate));
        }
        
        [Fact]
        public void given_xcoordinateEquals20_when_LocationIsOnGrid_then_return_False()
        {
            Coordinate coordinate = new Coordinate(20, 0);
            
            Assert.False(_validations.LocationIsOnGrid(20, coordinate));
        }
        
        [Fact]
        public void given_ycoordinateEquals20_when_LocationIsOnGrid_then_return_False()
        {
            Coordinate coordinate = new Coordinate(0, 20);
            
            Assert.False(_validations.LocationIsOnGrid(20, coordinate));
        }
    }
}