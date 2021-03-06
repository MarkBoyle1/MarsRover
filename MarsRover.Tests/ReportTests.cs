using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class ReportTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        private ReportBuilder _reportBuilder = new ReportBuilder();
        
        [Fact]
        public void given_finalSurfaceHasTwoObstacles_and_initialSurfaceHasOneObstacle_when_CreateReport_then_ObstaclesDiscoveredEqualsOne()
        {
            IMarsSurfaceBuilder _marsSurfaceBuilderFirst = new MarsSurfaceBuilder(new List<Coordinate>() { new Coordinate(1,1)}, 20);
            MarsSurface _initialSurface = _marsSurfaceBuilderFirst.CreateSurface();
            
            List<Coordinate> obstacles = new List<Coordinate>() {new Coordinate(3, 3), new Coordinate(2,2)};
            IMarsSurfaceBuilder _marsSurfaceBuilderSecond = new MarsSurfaceBuilder(obstacles, 20);
            MarsSurface _finalSurface = _marsSurfaceBuilderSecond.CreateSurface();

            int distanceTravelled = 10;
            ObjectLocation finalLocation = new ObjectLocation(new Coordinate(1, 1), Direction.South, DisplaySymbol.RoverSouthFacing);

            Report report =
                _reportBuilder.CreateReport(distanceTravelled, _initialSurface, _finalSurface, finalLocation);
            
            Assert.Equal(1, report.ObstaclesDiscovered);
        }
        
        [Fact]
        public void given_finalSurfaceHasOneObstacle_and_initialSurfaceHasTwoObstacles_when_CreateReport_then_ObstaclesDestroyedEqualsOne()
        {
            IMarsSurfaceBuilder _marsSurfaceBuilderFirst = new MarsSurfaceBuilder(new List<Coordinate>() { new Coordinate(1,1), new Coordinate(2,2)}, 20);
            MarsSurface _initialSurface = _marsSurfaceBuilderFirst.CreateSurface();
            
            List<Coordinate> obstacles = new List<Coordinate>() {new Coordinate(3, 3)};
            IMarsSurfaceBuilder _marsSurfaceBuilderSecond = new MarsSurfaceBuilder(obstacles, 20);
            MarsSurface _finalSurface = _marsSurfaceBuilderSecond.CreateSurface();

            int distanceTravelled = 10;
            ObjectLocation finalLocation = new ObjectLocation(new Coordinate(1, 1), Direction.South, DisplaySymbol.RoverSouthFacing);

            Report report =
                _reportBuilder.CreateReport(distanceTravelled, _initialSurface, _finalSurface, finalLocation);
            
            Assert.Equal(1, report.ObstaclesDestroyed);
        }

        [Fact]
        public void given_commandsContainsFourf_when_RunProgram_then_DistanceTravelledEquals4()
        {
            string[] args = new[] {"location:0,0,e", "commands:f,f,f,r,f", "obstacles:5,0", "mode:explore"};

            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(4, report.DistanceTravelled);
        }
    }
}