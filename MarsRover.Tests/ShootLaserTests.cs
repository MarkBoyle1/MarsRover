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
        public void given_roverFacingObstacle_and_InstructionEqualsShootLaser_when_ExecuteCommand_then_DisplaySymbolEqualsExplosion()
        {
            string[] args = new[] {"location:1,1,e", "obstacles:2,1", "commands:s", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            IMarsSurfaceBuilder marsSurfaceBuilder = planetSettings.MarsSurfaceBuilder;
            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            
            RoverBehaviour roverBehaviour = new RoverBehaviour();

            ObjectLocation objectLocation = roverBehaviour.ExecuteCommand(roverSettings.ObjectLocation, new Command(RoverInstruction.ShootLaser), surface);
            
            Assert.Equal(DisplaySymbol.Explosion, objectLocation.Symbol);
        }
        
        [Fact]
        public void given_roverFacingEast_and_InstructionEqualsShootLaser_when_ExecuteCommand_then_DisplaySymbolEqualsHorizontalLaser()
        {
            string[] args = new[] {"location:1,1,e", "obstacles:5,1", "commands:s", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            IMarsSurfaceBuilder marsSurfaceBuilder = planetSettings.MarsSurfaceBuilder;
            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            
            RoverBehaviour roverBehaviour = new RoverBehaviour();

            ObjectLocation objectLocation = roverBehaviour.ExecuteCommand(roverSettings.ObjectLocation, new Command(RoverInstruction.ShootLaser), surface);
            
            Assert.Equal(DisplaySymbol.LaserHorizontal, objectLocation.Symbol);
        }
        
        [Fact]
        public void given_roverFacingNorth_and_InstructionEqualsShootLaser_when_ExecuteCommand_then_DisplaySymbolEqualsVerticalLaser()
        {
            string[] args = new[] {"location:1,2,n", "obstacles:5,1", "commands:s", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine _engine = new Engine(roverSettings, planetSettings);

            IMarsSurfaceBuilder marsSurfaceBuilder = planetSettings.MarsSurfaceBuilder;
            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            
            RoverBehaviour roverBehaviour = new RoverBehaviour();

            ObjectLocation objectLocation = roverBehaviour.ExecuteCommand(roverSettings.ObjectLocation, new Command(RoverInstruction.ShootLaser), surface);
            
            Assert.Equal(DisplaySymbol.LaserVertical, objectLocation.Symbol);
        }
        
        [Fact]
        public void given_roverFacingEdgeOfGrid_and_InstructionEqualsShootLaser_when_ExecuteCommand_then_DisplaySymbolEqualsFreeSpace()
        {
            string[] args = new[] {"location:0,0,n", "obstacles:5,1", "commands:s", "mode:explore"};
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);

            IMarsSurfaceBuilder marsSurfaceBuilder = planetSettings.MarsSurfaceBuilder;
            MarsSurface surface = marsSurfaceBuilder.CreateSurface();
            
            RoverBehaviour roverBehaviour = new RoverBehaviour();

            ObjectLocation objectLocation = roverBehaviour.ExecuteCommand(roverSettings.ObjectLocation, new Command(RoverInstruction.ShootLaser), surface);
            
            Assert.Equal(DisplaySymbol.FreeSpace, objectLocation.Symbol);
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
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverSettings.ObjectLocation.Coordinate, DisplaySymbol.RoverEastFacing);
           
            Report report = _engine.RunProgram();
            
            Assert.Equal(surface.Surface, report.CurrentSurface.Surface);
            Assert.Equal(DisplaySymbol.RoverEastFacing, report.CurrentSurface.GetPoint(new Coordinate(1,1)));
        }
    }
}