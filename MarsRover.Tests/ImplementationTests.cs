using System;
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
            string[] obstacles = new []{"8,8"};

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
            string[] obstacles = new[] {"2,2"};
            
            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
            
            Assert.Equal(2, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(1, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroZeroNorth_when_MoveForward_then_return_ZeroNineNorth()
        {
            string[] startingPoint = new[] {"0", "0", "N"};
            string[] commands = new[] {"f"};
            string[] obstacles = new []{"8,8"};
            
            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
            
            Assert.Equal(0, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(9, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.North, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroNineSouth_when_MoveForward_then_return_ZeroZeroSouth()
        {
            string[] startingPoint = new[] {"0", "9", "S"};
            string[] commands = new[] {"f"};
            string[] obstacles = new[] {"8,8"};
            
            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
            
            Assert.Equal(0, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(0, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsNineNineEast_when_MoveForward_then_return_ZeroNineEast()
        {
            string[] startingPoint = new[] {"9", "9", "E"};
            string[] commands = new[] {"f"};
            string[] obstacles = Array.Empty<string>();
            
            RoverLocation roverLocation = _engine.RunProgram(startingPoint, commands, obstacles);
            
            Assert.Equal(0, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(9, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.East, roverLocation.DirectionFacing);
        }
    }
}