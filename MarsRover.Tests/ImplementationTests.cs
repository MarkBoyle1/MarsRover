using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ImplementationTests
    {
        private Engine _engine = new Engine();
        [Fact]
        public void given_inputEqualsCommandsMovingToThreeTwo_when_RunProgram_then_returns_LocationEqualsThreeTwo()
        {
            string[] startingPoint = new[] {"1", "1", "N"};
            string[] commands = new[] {"r", "f", "r", "f", "f"};
            List<Coordinate> obstacles = new List<Coordinate>();

            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
            
            Assert.Equal(2, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(3, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverWillMeetObstacleAtTwoTwo_when_RunProgram_then_returns_LocationEqualsTwoOne()
        {
            string[] startingPoint = new[] {"1", "1", "N"};
            string[] commands = new[] {"r", "f", "r", "f", "f"};
            Coordinate obstacle1 = new Coordinate(2, 2);
            List<Coordinate> obstacles = new List<Coordinate>(){obstacle1};
            
            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
            
            Assert.Equal(2, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(1, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, roverLocation.DirectionFacing);
        }
        
    }
}