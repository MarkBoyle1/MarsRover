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
        private ReportBuilder _reportBuilder;
        private RoverBehaviour _roverBehaviour;
        private Validations _validations;
        private Output _output;
        private IObjective _objective;
        private RoverSettings _roverSettings;
        private PlanetSettings _planetSettings;
        private MarsSurface _initialSurface;
        private int _distancedTravelled;
        private LaserShot _laserShot;
        private UtilityMethods _utility;
        
        public Engine(RoverSettings roverSettings, PlanetSettings planetSettings)
        {
            _roverSettings = roverSettings;
            _planetSettings = planetSettings;
            _marsSurfaceBuilder = _planetSettings.MarsSurfaceBuilder;
            _objective = _roverSettings.Objective;
            _utility = new UtilityMethods();
            _laserShot = new LaserShot(_marsSurfaceBuilder);
            _output = new Output();
            _validations = new Validations();
            _roverBehaviour = new RoverBehaviour();
            _reportBuilder = new ReportBuilder();
        }
        
        public Report RunProgram()
        {
            RoverLocation roverLocation = _roverSettings.RoverLocation;
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverLocation.Coordinate, _utility.DetermineDirectionOfRover(roverLocation.DirectionFacing));
            _initialSurface = surface;
            _output.DisplaySurface(surface, 500);

            Command command = _objective.ReceiveCommand(surface, roverLocation);
            Report report = ActivateRover(surface, roverLocation, command);

            _output.DisplayReport(report);
            
            return report;
        }
        
        private Report ActivateRover(MarsSurface surface, RoverLocation roverLocation, Command command)
        {
            RoverLocation newLocation = roverLocation;
            
            while (command.Instruction != RoverInstruction.Stop)
            {
                if (command.Instruction == RoverInstruction.ShootLaser)
                {
                    surface = FireGun(surface, roverLocation.Coordinate, roverLocation.DirectionFacing);
                }
                else
                {
                    newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);

                    if (_validations.LocationContainsObstacle(surface, newLocation))
                    {
                        _output.DisplayMessage(OutputMessages.ObstacleFound);
                        command = _objective.ReceiveCommandForObstacle();
                        continue;
                    }
                    
                    UpdateDistanceTravelled(command);
                    
                    surface = MoveRover(surface, roverLocation, newLocation);
                    roverLocation = newLocation;
                }

                command = _objective.ReceiveCommand(surface, roverLocation);
            
                _output.DisplaySurface(surface, 500);
            }
            
            return _reportBuilder.CreateReport(_distancedTravelled, _initialSurface, surface, roverLocation);
        }
        
        public MarsSurface FireGun(MarsSurface surface, Coordinate coordinate, Direction direction)
        {
            string symbolForOldLocation =
                _utility.SpaceNeedsToBeCleared(surface, coordinate, _utility.DetermineDirectionOfRover(direction));
            surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, symbolForOldLocation);
            
            LaserBeam laserBeam = _laserShot.UpdateLaserShot(surface, coordinate, direction);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, laserBeam.Coordinate, laserBeam.Symbol);
            
            _output.DisplaySurface(surface, 100);
            
            if (laserBeam.Symbol == DisplaySymbol.FreeSpace)
            {
                return surface;
            }

            return FireGun(surface, laserBeam.Coordinate, direction);
        }
        
        private MarsSurface MoveRover(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation)
        {
            surface = _marsSurfaceBuilder.UpdateSurface(surface, oldLocation.Coordinate, DisplaySymbol.FreeSpace);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate,
                _utility.DetermineDirectionOfRover(newLocation.DirectionFacing));
            Coordinate nextLocation =
                _utility.GetNextSpace(newLocation.Coordinate, newLocation.DirectionFacing);
            nextLocation = _utility.WrapAroundPlanetIfRequired(nextLocation);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, nextLocation,
                _utility.RevealSpaceInFrontOfRover(surface, nextLocation));

            return surface;
        }

        private void UpdateDistanceTravelled(Command command)
        {
            if (command.Instruction is RoverInstruction.MoveForward or RoverInstruction.MoveBack)
            {
                _distancedTravelled += 1;
            }
        }
    }
}