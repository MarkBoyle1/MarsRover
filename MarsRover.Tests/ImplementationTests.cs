using MarsRover.Objectives;
using Xunit;

namespace MarsRover.Tests
{
    public class ImplementationTests
    {
        private RoverSettings _defaultRoverSettings;
        private PlanetSettings _defaultplanPlanetSettings;
        private InputProcessor _inputProcesser = new InputProcessor();

        [Fact]
        public void given_roverWillMeetObstacleAtTwoTwo_when_RunProgram_then_returns_LocationEqualsTwoOne()
        {
            string[] args = new[] {"location:1,1,n", "commands:r,f,r,f,f", "obstacles:5,5", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(2, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(3, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_roverLocationEqualsZeroZeroNorth_when_MoveForward_then_return_ZeroNineNorth()
        {
            string[] args = new[] {"location:0,0,n", "commands:f", "obstacles:8,8", "mode:explore"};

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
           string[] args = new[] {"location:0,19,s", "commands:f", "obstacles:8,8", "mode:explore"};

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
            string[] args = new[] {"location:19,19,e", "commands:f", "obstacles:2,2", "mode:explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(0, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(19, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.East, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_gridSizeEquals1_and_objectiveEqualsMap_when_RunProgram_then_areasDiscoveredEquals1()
        {
            string[] args = new[] {"location:0,0,e", "mode:map", "gridsize:1"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(1, report.CurrentSurface.AreasDiscovered);
        }
        
        [Fact]
        public void given_surfaceIsBlank_and_objectiveEqualsDestroyer_when_CheckForCompletion_then_return_true()
        {
            IMarsSurfaceBuilder marsSurfaceBuilder = new TestBlankSurfaceBuilder();
            ReportBuilder reportBuilder = new ReportBuilder();
            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            ObjectLocation roverLocation = new ObjectLocation(new Coordinate(0, 0), Direction.North);

            IObjective objective = new Destroyer(100);
            Report report = reportBuilder.CreateReport(0, surface, surface, roverLocation);

            Assert.True(objective.CheckForCompletion(report));
        }

        [Fact]
        public void given_roverMeetsObstaclesBeforeFinishingCommands_when_RunProgram_then_RoverStopsAtObstacle()
        {
            string[] args = new[] {"location:0,0,e", "commands:f,f,f,r,f", "obstacles:8,8", "mode:explore"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(3, report.FinalLocation.Coordinate.XCoordinate);
            Assert.Equal(1, report.FinalLocation.Coordinate.YCoordinate);
            Assert.Equal(Direction.South, report.FinalLocation.DirectionFacing);
        }
        
        [Fact]
        public void given_maxDistanceEquals5_when_RunProgram_then_distanceTravelledEquals5()
        {
            string[] args = new[] {"location:0,0,e", "obstacles:3,3", "mode:map", "maxdistance:3"};

            RoverSettings roverSettings = _inputProcesser.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcesser.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);
            
            Report report = _engine.RunProgram();
            
            Assert.Equal(3, report.DistanceTravelled);
        }
    }
}