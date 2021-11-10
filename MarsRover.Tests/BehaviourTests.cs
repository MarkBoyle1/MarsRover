using Xunit;

namespace MarsRover.Tests
{
    public class BehaviourTests
    {
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        
        [Fact]
        public void given_locationEqualsL1and1andN_and_commandEqualsTurnLeft_when_ExecuteCommand_then_LocationEquals1and1andW()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.North);
            Command command = new Command(RoverInstruction.TurnLeft);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            Direction expectResult = Direction.West;

            Direction actualResult = roverLocation.DirectionFacing;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andE_and_commandEqualsTurnRight_when_ExecuteCommand_then_LocationEquals1and1andS()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.East);
            Command command = new Command(RoverInstruction.TurnRight);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            Direction expectResult = Direction.South;

            Direction actualResult = roverLocation.DirectionFacing;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andN_and_commandEqualsMoveForward_when_ExecuteCommand_then_LocationEquals1and2andN()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.North);
            Command command = new Command(RoverInstruction.MoveForward);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            int expectResult = 0;

            int actualResult = roverLocation.Coordinate.YCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and4andN_and_commandEqualsMoveBack_when_ExecuteCommand_then_LocationEquals1and3andN()
        {
            Coordinate coordinate = new Coordinate(1, 4);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.North);
            Command command = new Command(RoverInstruction.MoveBack);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            int expectResult = 5;

            int actualResult = roverLocation.Coordinate.YCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andE_and_commandEqualsMoveForward_when_ExecuteCommand_then_LocationEquals2and1andE()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            RoverLocation roverLocation = new RoverLocation(coordinate, Direction.East);
            Command command = new Command(RoverInstruction.MoveForward);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            int expectResult = 2;

            int actualResult = roverLocation.Coordinate.XCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
    }
}