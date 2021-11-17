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
        private ReportBuilder _reportBuilder = new ReportBuilder();
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        private Validations _validations = new Validations();
        private Output _output = new Output();
        private Random _random = new Random();
        private IObjective _objective;
        private const int SizeOfGrid = 20;
        private RoverSettings _roverSettings;
        private PlanetSettings _planetSettings;
        private MarsSurface _initialSurface;
        private MarsSurface _finalSurface;
        private int _distancedTravelled;
        private LaserShot _laserShot = new LaserShot();
        private UtilityMethods _utility = new UtilityMethods();
        
        public Engine(RoverSettings roverSettings, PlanetSettings planetSettings)
        {
            _roverSettings = roverSettings;
            _planetSettings = planetSettings;
            _marsSurfaceBuilder = _planetSettings.MarsSurfaceBuilder;
            _objective = _roverSettings.Objective;
        }
        
        public Report RunProgram()
        {
            RoverLocation roverLocation = _roverSettings.RoverLocation;
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverLocation.Coordinate, _utility.DetermineDirectionOfRover(roverLocation.DirectionFacing));
            _initialSurface = surface;
            _output.DisplaySurface(surface, 500);

            RoverLocation finalDestination = ActivateRover(surface, roverLocation);

            Report report =
                _reportBuilder.CreateReport(_distancedTravelled, _initialSurface, _finalSurface, finalDestination);

            _output.DisplayReport(report);
            
            return report;
        }

        public RoverLocation ActivateRover(MarsSurface surface, RoverLocation roverLocation)
        {
            Command command = _objective.ReceiveCommand(surface, roverLocation);

            while (command.Instruction != RoverInstruction.Stop)
            {
                RoverLocation oldLocation = roverLocation;

                if (command.Instruction == RoverInstruction.ShootLaser)
                {
                    // surface = _laserShot.FireGun(surface, roverLocation);
                    surface = FireGun(surface, roverLocation.Coordinate, roverLocation.DirectionFacing);
                }
                else
                {
                    RoverLocation newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);
                    
                    if (_validations.LocationContainsObstacle(surface, newLocation))
                    {
                        _output.DisplayMessage(OutputMessages.ObstacleFound);
                        command = _objective.ReceiveCommandForObstacle();
                        
                        if (command.Instruction == RoverInstruction.Stop)
                        {
                            break;
                        }
                        
                        newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);
                    }

                    if (command.Instruction is RoverInstruction.MoveForward or RoverInstruction.MoveBack)
                    {
                        _distancedTravelled += 1;
                    }

                    roverLocation = newLocation;
                   
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, oldLocation.Coordinate, DisplaySymbol.FreeSpace);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate, _utility.DetermineDirectionOfRover(newLocation.DirectionFacing));
                    Coordinate nextLocation = _utility.GetNextSpace(roverLocation.Coordinate, roverLocation.DirectionFacing);
                    nextLocation = _utility.WrapAroundPlanetIfRequired(nextLocation);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, nextLocation, _utility.RevealSpaceInFrontOfRover(surface, nextLocation));

                    _output.DisplaySurface(surface, 500);
                }

                command = _objective.ReceiveCommand(surface, roverLocation);
            }

            _finalSurface = surface;
            
            return roverLocation;
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
    }
}