using System;
using Xunit;

namespace MarsRover.Tests
{
    public class CommandTests
    {
        private InputProcessor _inputProcessor = new InputProcessor();
        
        [Fact]
        public void given_inputEqualsL_when_TurnInputIntoCommand_then_CommandEqualsTurnLeft()
        {
            Command command = _inputProcessor.TurnInputIntoCommand("l");

            string actualResult = command.Instruction.ToString();

            string expectResult = "TurnLeft";
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_inputEqualsR_when_TurnInputIntoCommand_then_CommandEqualsTurnRight()
        {
            Command command = _inputProcessor.TurnInputIntoCommand("r");

            string actualResult = command.Instruction.ToString();

            string expectResult = "TurnRight";
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_inputEqualsF_when_TurnInputIntoCommand_then_CommandEqualsMoveForward()
        {
            Command command = _inputProcessor.TurnInputIntoCommand("f");

            string actualResult = command.Instruction.ToString();

            string expectResult = "MoveForward";
            
            Assert.Equal(expectResult, actualResult);
        }
        
        [Fact]
        public void given_inputEqualsB_when_TurnInputIntoCommand_then_CommandEqualsMoveBack()
        {
            Command command = _inputProcessor.TurnInputIntoCommand("b");

            string actualResult = command.Instruction.ToString();

            string expectResult = "MoveBack";
            
            Assert.Equal(expectResult, actualResult);
        }
    }
}