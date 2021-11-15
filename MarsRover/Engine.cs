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
        private RoverBehaviour _roverBehaviour = new RoverBehaviour();
        private Validations _validations = new Validations();
        private Output _output = new Output();
        private Random _random = new Random();
        private IObjective _objective;
        private const int SizeOfGrid = 20;
        private RoverSettings _roverSettings;
        private PlanetSettings _planetSettings;

        public Engine(RoverSettings roverSettings, PlanetSettings planetSettings)
        {
            _roverSettings = roverSettings;
            _planetSettings = planetSettings;
            _marsSurfaceBuilder = _planetSettings.MarsSurfaceBuilder;
            _objective = _roverSettings.Objective;
        }
        
        public RoverLocation RunProgram()
        {
            // RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            // PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            
            RoverLocation roverLocation = _roverSettings.RoverLocation;
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverLocation.Coordinate, DetermineDirectionOfRover(roverLocation.DirectionFacing));
            _output.DisplaySurface(surface, 500);

            RoverLocation finalDestination = ActivateRover(surface, roverLocation);

            return finalDestination;
        }

        public RoverLocation ActivateRover(MarsSurface surface, RoverLocation roverLocation)
        {
            Command command = _objective.ReceiveCommand(surface, roverLocation);

            while (command.Instruction != RoverInstruction.Stop)
            {
                RoverLocation oldLocation = roverLocation;

                if (command.Instruction == RoverInstruction.ShootLaser)
                {
                    surface = FireGun(surface, roverLocation);
                }
                else
                {
                    RoverLocation newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);


                    if (_validations.LocationContainsObstacle(surface, newLocation))
                    {
                        _output.DisplayMessage(OutputMessages.ObstacleFound);
                        command = _objective.ReceiveCommandForObstacle();
                        newLocation = _roverBehaviour.ExecuteCommand(roverLocation, command);
                    }

                    roverLocation = newLocation;
                    // surface = _marsSurfaceBuilder.UpdateRoverMovement(surface, oldLocation, newLocation);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, oldLocation.Coordinate, DisplaySymbol.FreeSpace);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate, DetermineDirectionOfRover(newLocation.DirectionFacing));
                    Coordinate nextLocation = GetNextSpace(roverLocation.Coordinate, roverLocation.DirectionFacing);
                    nextLocation = WrapAroundPlanetIfRequired(nextLocation);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, nextLocation, RevealSpaceInFrontOfRover(surface, nextLocation));

                    _output.DisplaySurface(surface, 500);
                }

                command = _objective.ReceiveCommand(surface, roverLocation);
            }

            return roverLocation;
        }

        public MarsSurface FireGun(MarsSurface surface, RoverLocation roverLocation)
        {
            string displaySymbol = roverLocation.DirectionFacing is Direction.North or Direction.South ? DisplaySymbol.LaserVertical : DisplaySymbol.LaserHorizontal;
            string RoverImage = DetermineDirectionOfRover(roverLocation.DirectionFacing);
            Coordinate coordinate = roverLocation.Coordinate;
            Coordinate nextSpace = GetNextSpace(roverLocation.Coordinate, roverLocation.DirectionFacing);
            
            if (!_validations.LocationIsOnGrid(20, nextSpace))
            {
                return surface;
            }

            while (displaySymbol != DisplaySymbol.FreeSpace)
            {
                if (surface.GetPoint(nextSpace) == DisplaySymbol.FreeSpace || !_validations.LocationIsOnGrid(20, nextSpace))
                {
                    // surface = _marsSurfaceBuilder.UpdateLaserShot(surface, coordinate, nextSpace, displaySymbol);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, SpaceNeedsToBeCleared(surface, coordinate, RoverImage));
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, nextSpace, displaySymbol);
                    
                    coordinate = nextSpace;
                    nextSpace = GetNextSpace(coordinate, roverLocation.DirectionFacing);
                    _output.DisplaySurface(surface, 100);
                }
                else if (surface.GetPoint(nextSpace) == DisplaySymbol.Obstacle)
                {
                    Console.Beep();
                    displaySymbol = DisplaySymbol.Explosion;
                    // surface = _marsSurfaceBuilder.UpdateLaserShot(surface, coordinate, nextSpace, displaySymbol);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, SpaceNeedsToBeCleared(surface, coordinate, RoverImage));
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, nextSpace, displaySymbol);
                    
                    _output.DisplaySurface(surface, 300);
                    _output.DisplaySurface(surface, 300);
                    _output.DisplaySurface(surface, 300);
                    displaySymbol = DisplaySymbol.FreeSpace;
                    // return _marsSurfaceBuilder.UpdateLaserShot(surface, coordinate, nextSpace, displaySymbol);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, SpaceNeedsToBeCleared(surface, coordinate, RoverImage));
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, nextSpace, displaySymbol);
                    
                    return surface;
                }
                
                if (!_validations.LocationIsOnGrid(20, nextSpace))
                {
                    // displaySymbol = ".";
                    // surface = _marsSurfaceBuilder.UpdateLaserShot(surface, coordinate, coordinate, displaySymbol);
                    surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, SpaceNeedsToBeCleared(surface, coordinate, RoverImage));
                    
                    return surface;
                }
            }

            return surface;
        }

        private string SpaceNeedsToBeCleared(MarsSurface surface, Coordinate coordinate, string roverImage)
        {
            return surface.Surface[coordinate.YCoordinate][coordinate.XCoordinate] == roverImage
                ? roverImage
                : DisplaySymbol.FreeSpace;
        }

        private Coordinate GetNextSpace(Coordinate coordinate, Direction direction)
        {
            Coordinate newCoordinate = coordinate;
            
            switch (direction)
            {
                case Direction.North:
                     return new Coordinate(coordinate.XCoordinate, coordinate.YCoordinate - 1);
                case Direction.East:
                    return new Coordinate(coordinate.XCoordinate + 1, coordinate.YCoordinate);
                case Direction.South:
                    return new Coordinate(coordinate.XCoordinate, coordinate.YCoordinate + 1);
                case Direction.West:
                    return new Coordinate(coordinate.XCoordinate - 1, coordinate.YCoordinate);
                default:
                    throw new Exception();
            }
        }
        
        private string DetermineDirectionOfRover(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return DisplaySymbol.RoverNorthFacing;
                case Direction.East:
                    return DisplaySymbol.RoverEastFacing;
                case Direction.South:
                    return DisplaySymbol.RoverSouthFacing;
                case Direction.West:
                    return DisplaySymbol.RoverWestFacing;
                default:
                    throw new Exception();
            }
        }

        private string RevealSpaceInFrontOfRover(MarsSurface surface, Coordinate coordinate)
        {
            string revealedSpace = surface.Surface[coordinate.YCoordinate][coordinate.XCoordinate];
            if (revealedSpace == " ")
            {
                int randomNumber = _random.Next(1, 11);
                return randomNumber > 2 ? DisplaySymbol.FreeSpace : DisplaySymbol.Obstacle;
            }
            
            return revealedSpace;
        }

        private Coordinate WrapAroundPlanetIfRequired(Coordinate coordinate)
        {
            int xCoordinate = AdjustIndividualCoordinate(coordinate.XCoordinate);
            int yCoordinate = AdjustIndividualCoordinate(coordinate.YCoordinate);

            return new Coordinate(xCoordinate, yCoordinate);
        }

        private int AdjustIndividualCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                return SizeOfGrid - 1;
            }
            
            if (coordinate > SizeOfGrid - 1)
            {
                return 0;
            }
            
            return coordinate;
        }
    }
}