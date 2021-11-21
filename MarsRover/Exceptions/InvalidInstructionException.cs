using System;

namespace MarsRover.Exceptions
{
    public class InvalidInstructionException : Exception
    {
        public InvalidInstructionException(string input)
            : base(String.Format("Invalid Instruction: {0}", input))
        {

        }
    }
}