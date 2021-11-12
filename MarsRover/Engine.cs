using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Objectives;
using Microsoft.VisualBasic;

namespace MarsRover
{
    public class Engine
    {
        private IMarsSurfaceBuilder _marsSurfaceBuilder;
        private InputProcessor _inputProcessor = new InputProcessor();
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        private Validations _validations = new Validations();
        private Output _output = new Output();
        private IObjective _objective;
        
        public RoverLocation RunProgram(string[] args)
        {
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            
            _objective = roverSettings.Objective;
            RoverLocation roverLocation = roverSettings.RoverLocation;
            _marsSurfaceBuilder = planetSettings.MarsSurfaceBuilder;


            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.PlaceRoverOnStartingPoint(surface, roverLocation);
            _output.DisplaySurface(surface);

            RoverLocation finalDestination = ActivateRover(surface, roverLocation);

            return finalDestination;
        }

        public RoverLocation ActivateRover(MarsSurface surface, RoverLocation roverLocation)
        {
            Command command = _objective.ReceiveCommand(surface, roverLocation);

            while (command.Instruction != RoverInstruction.Stop)
            {
                RoverLocation oldLocation = roverLocation;
                RoverLocation newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);
                
                if(_validations.LocationContainsObstacle(surface, newLocation))
                {
                    _output.DisplayMessage(OutputMessages.ObstacleFound);
                    command = _objective.ReceiveCommandForObstacle();
                    newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);
                }

                roverLocation = newLocation;
                surface = _marsSurfaceBuilder.UpdateRoverMovement(surface, oldLocation, newLocation);
                _output.DisplaySurface(surface);
                
                command = _objective.ReceiveCommand(surface, roverLocation);
            }

            return roverLocation;
        }
    }
}