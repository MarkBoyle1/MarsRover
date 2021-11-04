using Xunit;

namespace MarsRover.Tests
{
    public class BehaviourTests
    {
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        
        [Fact]
        public void given_locationEqualsL1and1andN_and_commandEqualsTurnLeft_when_ExecuteCommand_then_LocationEquals1and1andW()
        {
            RoverLocation roverLocation = new RoverLocation(1, 1, Direction.North);
            Command command = new Command(RoverInstruction.TurnLeft);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            Direction expectResult = Direction.West;

            Direction actualResult = roverLocation.DirectionFacing;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andE_and_commandEqualsTurnRight_when_ExecuteCommand_then_LocationEquals1and1andS()
        {
            RoverLocation roverLocation = new RoverLocation(1, 1, Direction.East);
            Command command = new Command(RoverInstruction.TurnRight);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            Direction expectResult = Direction.South;

            Direction actualResult = roverLocation.DirectionFacing;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andN_and_commandEqualsMoveForward_when_ExecuteCommand_then_LocationEquals1and2andN()
        {
            RoverLocation roverLocation = new RoverLocation(1, 1, Direction.North);
            Command command = new Command(RoverInstruction.MoveForward);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            int expectResult = 2;

            int actualResult = roverLocation.YCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and4andN_and_commandEqualsMoveBack_when_ExecuteCommand_then_LocationEquals1and3andN()
        {
            RoverLocation roverLocation = new RoverLocation(1, 4, Direction.North);
            Command command = new Command(RoverInstruction.MoveBack);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            int expectResult = 3;

            int actualResult = roverLocation.YCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_locationEqualsL1and1andE_and_commandEqualsMoveForward_when_ExecuteCommand_then_LocationEquals2and1andE()
        {
            RoverLocation roverLocation = new RoverLocation(1, 1, Direction.East);
            Command command = new Command(RoverInstruction.MoveForward);
            
            roverLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

            int expectResult = 1;

            int actualResult = roverLocation.XCoordinate;
            
            Assert.Equal(expectResult, actualResult);
        }
    }
}