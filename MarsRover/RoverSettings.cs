using System.Collections.Generic;
using MarsRover.Objectives;

namespace MarsRover
{
    public class RoverSettings
    {
        public RoverLocation RoverLocation { get;  }
        public List<Command> Commands { get; }
        public IObjective Objective { get; }

        public RoverSettings(RoverLocation roverLocation, List<Command> commands, IObjective objective)
        {
            RoverLocation = roverLocation;
            Commands = commands;
            Objective = objective;
        }
    }
}