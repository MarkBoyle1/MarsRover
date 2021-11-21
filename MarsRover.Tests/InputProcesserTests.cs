using Xunit;

namespace MarsRover.Tests
{
    public class InputProcesserTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        
        [Fact]
        public void given_argsEqualsLocationOneOneN_when_GetRoverSettings_then_DirectionFacingEqualsNorth()
        {
            string[] args = new[] {"location:1,2,N"};

            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            
            Assert.Equal(Direction.North, roverSettings.RoverLocation.DirectionFacing);
            Assert.Equal(1, roverSettings.RoverLocation.Coordinate.XCoordinate);
            Assert.Equal(2, roverSettings.RoverLocation.Coordinate.YCoordinate);
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
        
        [Fact]
        public void given_obstaclesInputContainsThreeCoordinates_when_CreateSurface_then_thoseCoordinatesContainsObstacles()
        {
            string[] args = new[] {"obstacles:1,2;2,2;3,1"};

            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            IMarsSurfaceBuilder marsSurfaceBuilder = new MarsSurfaceBuilder(planetSettings.Obstacles, 20);
            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            
            Assert.Equal(DisplaySymbol.Obstacle, surface.GetPoint(new Coordinate(1,2)));
            Assert.Equal(DisplaySymbol.Obstacle, surface.GetPoint(new Coordinate(2,2)));
            Assert.Equal(DisplaySymbol.Obstacle, surface.GetPoint(new Coordinate(3,1)));
        }
    }
}