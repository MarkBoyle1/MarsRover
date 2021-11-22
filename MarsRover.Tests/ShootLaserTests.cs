using Xunit;

namespace MarsRover.Tests
{
    public class ShootLaserTests
    {
        private InputProcessor _inputProcessor;

        public ShootLaserTests()
        {
            _inputProcessor = new InputProcessor();
        }
        
        [Fact]
        public void given_obstacleAtFiveOne_and_roverAtOneOneEast_when_ShootLaser_then_FiveOneEqualsFreeSpace()
        {
            string[] args = new[] {"location:1,1,e", "obstacles:5,1", "commands:s", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            Report report = _engine.RunProgram();
            
            Assert.Equal(DisplaySymbol.FreeSpace, report.CurrentSurface.GetPoint(new Coordinate(5,1)));
            Assert.Equal(DisplaySymbol.RoverEastFacing, report.CurrentSurface.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_obstacleAtThreeThree_and_roverAtOneOneEast_when_ShootLaser_then_SurfaceDoesNotChange()
        {
            string[] args = new[] {"location:1,1,e", "obstacles:5,5", "commands:s", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            IMarsSurfaceBuilder _marsSurfaceBuilder = planetSettings.MarsSurfaceBuilder;
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverSettings.RoverLocation.Coordinate, ">");
           
            Report report = _engine.RunProgram();
            
            Assert.Equal(surface.Surface, report.CurrentSurface.Surface);
            Assert.Equal(DisplaySymbol.RoverEastFacing, report.CurrentSurface.GetPoint(new Coordinate(1,1)));
        }
    }
}