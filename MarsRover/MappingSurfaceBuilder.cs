using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class MappingSurfaceBuilder : IMarsSurfaceBuilder
    {
        private const string RoverNorthFacing = "^";
        private const string RoverEastFacing = ">";
        private const string RoverSouthFacing = "v";
        private const string RoverWestFacing = "<";
        private const string Obstacle = "x";
        private const string FreeSpace = ".";
        private const string UnknownSpace = " ";
        private Random random = new Random();
        private int SizeOfGrid;
        public MappingSurfaceBuilder(int sizeOfGrid)
        {
            SizeOfGrid = sizeOfGrid;
        }
        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[SizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[SizeOfGrid].Select(x => UnknownSpace).ToArray()
                )
                .ToArray();

            return new MarsSurface(surface);
        }
        
        public MarsSurface PlaceRoverOnStartingPoint(MarsSurface surface, RoverLocation startingPoint)
        {
            string rover = DetermineDirectionOfRover(startingPoint.DirectionFacing);

            surface.Surface[startingPoint.Coordinate.YCoordinate][startingPoint.Coordinate.XCoordinate] = rover;

            return surface;
        }

        public MarsSurface UpdateRoverMovement(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation)
        {
            surface.Surface[oldLocation.Coordinate.YCoordinate][oldLocation.Coordinate.XCoordinate] = FreeSpace;
            surface.Surface[newLocation.Coordinate.YCoordinate][newLocation.Coordinate.XCoordinate] = DetermineDirectionOfRover(newLocation.DirectionFacing);

            Coordinate coordinateOfSpaceInFrontOfRover = DetermineCoordinateOfSpaceInFrontOfRover(surface, newLocation);
            string revealedSpace = RevealSpaceInFrontOfRover(surface, coordinateOfSpaceInFrontOfRover);

            surface.Surface[coordinateOfSpaceInFrontOfRover.YCoordinate][coordinateOfSpaceInFrontOfRover.XCoordinate] =
                revealedSpace;
            return surface;
        }

        private Coordinate DetermineCoordinateOfSpaceInFrontOfRover(MarsSurface surface, RoverLocation location)
        {
            int xCoordinate = location.Coordinate.XCoordinate;
            int yCoordinate = location.Coordinate.YCoordinate;
            

            switch (location.DirectionFacing)
            {
                case Direction.North:
                    yCoordinate--;
                    break;
                case Direction.East:
                    xCoordinate++;
                    break;
                case Direction.South:
                    yCoordinate++;
                    break;
                case Direction.West:
                    xCoordinate--;
                    break;
                default:
                    throw new Exception();
            }
            
            xCoordinate = WrapAroundPlanetIfRequired(xCoordinate);
            yCoordinate = WrapAroundPlanetIfRequired(yCoordinate);

            return new Coordinate(xCoordinate, yCoordinate);
        }
        
        private string RevealSpaceInFrontOfRover(MarsSurface surface, Coordinate coordinate)
        {
            string revealedSpace = surface.Surface[coordinate.XCoordinate][coordinate.YCoordinate];
            if (revealedSpace == " ")
            {
                int randomNumber = random.Next(1, 11);
                return randomNumber > 3 ? FreeSpace : Obstacle;
            }
            
            return revealedSpace;
        }

        private int WrapAroundPlanetIfRequired(int coordinate)
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
        
        private string DetermineDirectionOfRover(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return RoverNorthFacing;
                case Direction.East:
                    return RoverEastFacing;
                case Direction.South:
                    return RoverSouthFacing;
                case Direction.West:
                    return RoverWestFacing;
                default:
                    throw new Exception();
            }
        }
    }
}