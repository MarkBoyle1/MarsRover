using System;
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
                new Destroyer()
            );

            _defaultplanPlanetSettings = new PlanetSettings
            (20,
                new List<Coordinate>(),
                new MarsSurfaceBuilder(obstacles)
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
            
            RoverLocation roverLocation = _engine.RunProgram();
            
            Assert.Equal(2, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(1, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroZeroNorth_when_MoveForward_then_return_ZeroNineNorth()
        {
            string[] args = new[] {"location:0,0,N", "commands:f", "obstacles:8,8", "explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            RoverLocation roverLocation = _engine.RunProgram();
            
            Assert.Equal(0, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(19, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.North, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroNineSouth_when_MoveForward_then_return_ZeroZeroSouth()
        {
           string[] args = new[] {"location:0,19,S", "commands:f", "obstacles:8,8", "explore"};

           RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
           PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
           Engine _engine = new Engine(roverSettings, planetSettings);
           
            RoverLocation roverLocation = _engine.RunProgram();
            
            Assert.Equal(0, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(0, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, roverLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsNineNineEast_when_MoveForward_then_return_ZeroNineEast()
        {
            string[] args = new[] {"location:19,19,E", "commands:f", "obstacles:2,2", "explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            RoverLocation roverLocation = _engine.RunProgram();
            
            Assert.Equal(0, roverLocation.Coordinate.XCoordinate);
            Assert.Equal(19, roverLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.East, roverLocation.DirectionFacing);
        }
    }
}