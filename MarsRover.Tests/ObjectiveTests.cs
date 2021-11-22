using System.Collections.Generic;
using MarsRover.Objectives;
using Xunit;

namespace MarsRover.Tests
{
    public class ObjectiveTests
    {
        private RoverLocation _defaultLocation;
        private IMarsSurfaceBuilder _marsSurfaceBuilder = new TestBlankSurfaceBuilder();
        
        public ObjectiveTests()
        {
            _defaultLocation = new RoverLocation(new Coordinate(1, 2), Direction.East, DisplaySymbol.RoverEastFacing);
        }
        
        [Fact]
        public void given_objectiveEqualsMapSurface_and_RoverMeetsObstacle_when_ExecuteCommand_then_commandDoesNotEqualStop()
        {
            IObjective objective = new MapSurface(1000);
        
            Command receivedCommand = objective.ReceiveCommandForObstacle();
        
            Assert.True(receivedCommand.Instruction != RoverInstruction.Stop);
        }
        
        [Fact]
        public void given_objectiveEqualsDestroyer_and_RoverMeetsObstacle_when_ExecuteCommand_then_commandDoesNotEqualStop()
        {
            IObjective objective = new Destroyer(1000);
        
            Command receivedCommand = objective.ReceiveCommandForObstacle();
        
            Assert.True(receivedCommand.Instruction != RoverInstruction.Stop);
        }
        
        [Fact]
        public void given_objectiveEqualsFollowCommands_and_RoverMeetsObstacle_when_ExecuteCommand_then_return_stop()
        {
            IObjective objective = new FollowCommands(new List<Command>());
        
            Command receivedCommand = objective.ReceiveCommandForObstacle();
        
            Assert.Equal(RoverInstruction.Stop, receivedCommand.Instruction);
        }
    }
}