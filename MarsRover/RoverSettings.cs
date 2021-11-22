using System.Collections.Generic;
using MarsRover.Objectives;

namespace MarsRover
{
    public class RoverSettings
    {
        public ObjectLocation ObjectLocation { get;  }
        public List<Command> Commands { get; }
        public IObjective Objective { get; }

        public RoverSettings(ObjectLocation objectLocation, List<Command> commands, IObjective objective)
        {
            ObjectLocation = objectLocation;
            Commands = commands;
            Objective = objective;
        }
    }
}