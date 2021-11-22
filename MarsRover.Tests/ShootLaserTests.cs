using Xunit;

namespace MarsRover.Tests
{
    public class ShootLaserTests
    {
        private RoverLocation _defaultLocation;
        private InputProcessor _inputProcessor;

        public ShootLaserTests()
        {
            _defaultLocation = new RoverLocation(new Coordinate(1, 1), Direction.East);
            _inputProcessor = new InputProcessor();
        }
        
        [Fact]
        public void given_obstacleAtFiveOne_and_roverAtOneOneEast_when_ShootLaser_then_FiveOneEqualsFreeSpace()
        {
            string[] args = new[] {"location:1,1,e", "obstacles:1,1", "mode:destroyer"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(planetSettings.Obstacles, 20);
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, new Coordinate(1,1), ">");
            LaserShot laserShot =
                new LaserShot(planetSettings.MarsSurfaceBuilder, new Output(20), new UtilityMethods(20), 20);
            surface = laserShot.FireGun(surface, _defaultLocation.Coordinate, _defaultLocation.DirectionFacing);
            
            Assert.Equal(DisplaySymbol.FreeSpace, surface.GetPoint(new Coordinate(1,5)));
            Assert.Equal(DisplaySymbol.RoverEastFacing, surface.GetPoint(new Coordinate(1,1)));
        }
        
        [Fact]
        public void given_obstacleAtThreeThree_and_roverAtOneOneEast_when_ShootLaser_then_SurfaceDoesNotChange()
        {
            string[] args = new[] {"location:1,1,e", "obstacles:3,3", "mode:destroyer"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(planetSettings.Obstacles, 20);
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, new Coordinate(1,1), ">");
            LaserShot laserShot =
                new LaserShot(planetSettings.MarsSurfaceBuilder, new Output(20), new UtilityMethods(20), 20);
            MarsSurface updatedSurface = laserShot.FireGun(surface, _defaultLocation.Coordinate, _defaultLocation.DirectionFacing);
            
            Assert.Equal(surface.Surface, updatedSurface.Surface);
        }
    }
}