using System.Collections.Generic;

namespace MarsRover
{
    public class Engine
    {
        private MarsSurfaceFactory _marsSurfaceFactory = new MarsSurfaceFactory();
        private InputProcessor _inputProcessor = new InputProcessor();
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        private Validations _validations = new Validations();
        public RoverLocation RunProgram(string[] startingLocation, string[] commands, List<Coordinate> obstacles)
        {
            RoverLocation roverLocation = _inputProcessor.DetermineStartingLocation(startingLocation);
            
            MarsSurface surface = _marsSurfaceFactory.CreateSurface(10, obstacles);
            surface = _marsSurfaceFactory.PlaceRoverOnStartingPoint(surface, roverLocation);
            
            List<Command> commandList = _inputProcessor.GetListOfCommands(commands);
            roverLocation = ExecuteCommands(surface, roverLocation, commandList);

            return roverLocation;
        }

        private RoverLocation ExecuteCommands(MarsSurface surface, RoverLocation roverLocation, List<Command> commandList)
        {
            RoverLocation updatedLocation = roverLocation;
            
            foreach (var command in commandList)
            {
                RoverLocation newLocation = _roverBehaviour.ExecuteCommand(updatedLocation, command);
                if (_validations.LocationContainsObstacle(surface, newLocation))
                {
                    //Report Obstacle
                    break;
                }

                updatedLocation = newLocation;
            }

            return updatedLocation;
        }
    }
}