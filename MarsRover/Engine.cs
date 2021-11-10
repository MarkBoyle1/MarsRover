using System.Collections.Generic;

namespace MarsRover
{
    public class Engine
    {
        private IMarsSurfaceBuilder _marsSurfaceBuilder;
        private InputProcessor _inputProcessor = new InputProcessor();
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        private Validations _validations = new Validations();
        private Output _output = new Output();
        public RoverLocation RunProgram(string[] startingLocation, string[] commands, string[] obstacles)
        {
            RoverLocation roverLocation = _inputProcessor.DetermineStartingLocation(startingLocation);
            List<Coordinate> obstacleCoordinates = _inputProcessor.TurnObstacleInputsIntoCoordinates(obstacles);
            List<Command> commandList = _inputProcessor.GetListOfCommands(commands);
            _marsSurfaceBuilder = new MarsSurfaceBuilder(obstacleCoordinates);
            // _marsSurfaceBuilder = new MappingSurfaceBuilder(20);


            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.PlaceRoverOnStartingPoint(surface, roverLocation);
            _output.DisplaySurface(surface);
            
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