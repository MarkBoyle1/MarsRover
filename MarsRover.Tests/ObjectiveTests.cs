using System.Collections.Generic;
using MarsRover.Objectives;
using Xunit;

namespace MarsRover.Tests
{
    public class ObjectiveTests
    {
        private RoverLocation _defaultLocation;
        private IMarsSurfaceBuilder _marsSurfaceBuilder = new TestMarsSurfaceBuilder();
        
        public ObjectiveTests()
        {
            _defaultLocation = new RoverLocation(new Coordinate(1, 2), Direction.East);
        }
        
        [Fact]
        public void given_objectiveEqualsMapSurface_and_RoverMeetsObstacle_when_ExecuteCommand_then_return_turnLeft()
        {
            IObjective objective = new MapSurface();
        
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