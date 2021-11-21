using System;

namespace MarsRover.Exceptions
{
    public class InvalidDirectionException : Exception
    {
        public InvalidDirectionException(string input)
            : base(String.Format("Invalid Direction: {0}", input))
        {

        }
    }
    
}