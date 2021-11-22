using Xunit;

namespace MarsRover.Tests
{
    public class BehaviourTests
    {
        private RoverBehaviour _roverBehaviour;
        private MarsSurface _surface;
        private IMarsSurfaceBuilder _marsSurfaceBuilder = new TestBlankSurfaceBuilder();

        public BehaviourTests()
        {
            _roverBehaviour = new RoverBehaviour();
            _surface = _marsSurfaceBuilder.CreateSurface();
        }
        
        [Fact]
        public void given_locationEqualsL1and1andN_and_commandEqualsTurnLeft_when_ExecuteCommand_then_LocationEquals1and1andW()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation roverLocation = new ObjectLocation(coordinate, Direction.North, DisplaySymbol.RoverNorthFacing);
            Command command = new Command(RoverInstruction.TurnLeft);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command, _surface);
        
            Direction expectResult = Direction.West;
        
            Direction actualResult = roverLocation.DirectionFacing;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andE_and_commandEqualsTurnRight_when_ExecuteCommand_then_LocationEquals1and1andS()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation objectLocation = new ObjectLocation(coordinate, Direction.East, DisplaySymbol.RoverEastFacing);
            Command command = new Command(RoverInstruction.TurnRight);
            
            objectLocation = _roverBehaviour.ExecuteCommand(objectLocation, command, _surface);
        
            Direction expectResult = Direction.South;
        
            Direction actualResult = objectLocation.DirectionFacing;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andN_and_commandEqualsMoveForward_when_ExecuteCommand_then_LocationEquals1and2andN()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation objectLocation = new ObjectLocation(coordinate, Direction.North, DisplaySymbol.RoverNorthFacing);
            Command command = new Command(RoverInstruction.MoveForward);
            
            objectLocation = _roverBehaviour.ExecuteCommand(objectLocation, command, _surface);
        
            int expectResult = 0;
        
            int actualResult = objectLocation.Coordinate.YCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and4andN_and_commandEqualsMoveBack_when_ExecuteCommand_then_LocationEquals1and3andN()
        {
            Coordinate coordinate = new Coordinate(1, 4);
            ObjectLocation objectLocation = new ObjectLocation(coordinate, Direction.North, DisplaySymbol.RoverNorthFacing);
            Command command = new Command(RoverInstruction.MoveBack);
            
            objectLocation = _roverBehaviour.ExecuteCommand(objectLocation, command, _surface);
        
            int expectResult = 5;
        
            int actualResult = objectLocation.Coordinate.YCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andE_and_commandEqualsMoveForward_when_ExecuteCommand_then_LocationEquals2and1andE()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            ObjectLocation objectLocation = new ObjectLocation(coordinate, Direction.East, DisplaySymbol.RoverEastFacing);
            Command command = new Command(RoverInstruction.MoveForward);
            
            objectLocation = _roverBehaviour.ExecuteCommand(objectLocation, command, _surface);
        
            int expectResult = 2;
        
            int actualResult = objectLocation.Coordinate.XCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
    }
}