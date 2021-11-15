using System.Collections.Generic;
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
            string[] args = new[] {"location:1,1,E", "obstacles:1,1", "destroyer"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            IMarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder(planetSettings.Obstacles);
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, new Coordinate(1,1), ">");
            
            surface = _engine.FireGun(surface, _defaultLocation);
            
            Assert.Equal(DisplaySymbol.FreeSpace, surface.GetPoint(new Coordinate(1,5)));
            Assert.Equal(DisplaySymbol.RoverEastFacing, surface.GetPoint(new Coordinate(1,1)));

        }
    }
}