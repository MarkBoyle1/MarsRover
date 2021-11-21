using System.Collections.Generic;
using MarsRover.Objectives;
using Xunit;

namespace MarsRover.Tests
{
    public class ImplementationTests
    {
        private RoverSettings _defaultRoverSettings;
        private PlanetSettings _defaultplanPlanetSettings;
        private Engine _engine;
        private InputProcessor _inputProcesser = new InputProcessor();

        public ImplementationTests()
        {
            List<Coordinate> obstacles = new List<Coordinate>() {new Coordinate(1,2)};
            _defaultRoverSettings = new RoverSettings
            (
                new RoverLocation(new Coordinate(1, 1), Direction.North),
                new List<Command>(),
                new Destroyer(1000)
            );

            _defaultplanPlanetSettings = new PlanetSettings
            (20,
                new List<Coordinate>(),
                new MarsSurfaceBuilder(obstacles, 20)
            );
            
            _engine = new Engine(_defaultRoverSettings, _defaultplanPlanetSettings);
        }
        
        [Fact]
        public void given_roverWillMeetObstacleAtTwoTwo_when_RunProgram_then_returns_LocationEqualsTwoOne()
        {
            string[] args = new[] {"location:1,1,N", "commands:r,f,r,f,f", "obstacles:2,2", "explore"};
            
            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(2, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(1, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroZeroNorth_when_MoveForward_then_return_ZeroNineNorth()
        {
            string[] args = new[] {"location:0,0,N", "commands:f", "obstacles:8,8", "explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(0, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(19, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.North, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroNineSouth_when_MoveForward_then_return_ZeroZeroSouth()
        {
           string[] args = new[] {"location:0,19,S", "commands:f", "obstacles:8,8", "explore"};

           RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
           PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
           Engine _engine = new Engine(roverSettings, planetSettings);
           
            Report report = _engine.RunProgram();
            
            Assert.Equal(0, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(0, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsNineNineEast_when_MoveForward_then_return_ZeroNineEast()
        {
            string[] args = new[] {"location:19,19,E", "commands:f", "obstacles:2,2", "explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(0, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(19, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.East, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void CompletesMappingMode()
        {
            IMarsSurfaceBuilder marsSurfaceBuilder = new MappingSurfaceBuilder(1);

            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            surface = marsSurfaceBuilder.UpdateSurface(surface, new Coordinate(0, 0), DisplaySymbol.RoverNorthFacing);

            IObjective objective = new MapSurface(1000);

            Assert.Equal(1, surface.AreasDiscovered);
        }

        [Fact]
        public void given_roverMeetsObstaclesBeforeFinishingCommands_when_RunProgram_then_RoverStopsAtObstacle()
        {
            string[] args = new[] {"location:0,0,E", "commands:f,f,f,r,f", "obstacles:2,0", "explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(1, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(0, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.East, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_maxDistanceEquals5_when_RunProgram_then_distanceTravelledEquals5()
        {
            string[] args = new[] {"location:0,0,E", "obstacles:2,0", "map", "maxdistance:5"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(5, report.DistanceTravelled);
        }
    }
}