using Xunit;

namespace MarsRover.Tests
{
    public class InputProcesserTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        
        [Fact]
        public void given_argsEqualsLocationOneOneN_when_GetRoverSettings_then_DirectionFacingEqualsNorth()
        {
            string[] args = new[] {"location:1,1,N"};

            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            
            Assert.Equal(Direction.North, roverSettings.RoverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_argsContainsThreeCommands_when_GetRoverSettings_then_return_ListWithThreeCommands()
        {
            string[] args = new[] {"commands:r,f,f"};

            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            
            Assert.Equal(3, roverSettings.Commands.Count);
        }
        
        [Fact]
        public void given_argsContainsThreeObstacles_when_GetRoverSettings_then_return_ListWithThreeObstacles()
        {
            string[] args = new[] {"obstacles:1,2;2,2;3,1"};

            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            
            Assert.Equal(3, planetSettings.Obstacles.Count);
        }
    }
}