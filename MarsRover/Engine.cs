using System.Collections.Generic;

namespace MarsRover
{
    public class Engine
    {
        private MarsSurfaceBuilder _marsSurfaceBuilder = new MarsSurfaceBuilder();
        private InputProcessor _inputProcessor = new InputProcessor();
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        private Validations _validations = new Validations();
        private Output _output = new Output();
        public RoverLocation RunProgram(string[] startingLocation, string[] commands, List<Coordinate> obstacles)
        {
            RoverLocation roverLocation = _inputProcessor.DetermineStartingLocation(startingLocation);
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface(10, obstacles);
            surface = _marsSurfaceBuilder.PlaceRoverOnStartingPoint(surface, roverLocation);
            _output.DisplaySurface(surface);
            
            List<Command> commandList = _inputProcessor.GetListOfCommands(commands);
            roverLocation = ExecuteCommands(surface, roverLocation, commandList);

            return roverLocation;
        }

        private RoverLocation ExecuteCommands(MarsSurface surface, RoverLocation roverLocation, List<Command> commandList)
        {
            RoverLocation updatedLocation = roverLocation;
            
            foreach (var command in commandList)
            {
                RoverLocation oldLocation = updatedLocation;
                
                RoverLocation newLocation = _roverBehaviour.ExecuteCommand(updatedLocation, command);
                if (_validations.LocationContainsObstacle(surface, newLocation))
                {
                    _output.DisplayMessage(OutputMessages.ObstacleFound);
                    return updatedLocation;
                }

                updatedLocation = newLocation;
                surface = _marsSurfaceBuilder.UpdateRoverMovement(surface, oldLocation, newLocation);
                _output.DisplaySurface(surface);
            }

            return updatedLocation;
        }
    }
}