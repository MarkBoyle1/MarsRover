using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class MarsSurfaceBuilder
    {
        private const string RoverNorthFacing = "^";
        private const string RoverEastFacing = ">";
        private const string RoverSouthFacing = "v";
        private const string RoverWestFacing = "<";
        private const string Obstacle = "x";
        private const string FreeSpace = ".";

        public MarsSurface CreateSurface(int sizeOfGrid, List<Coordinate> obstacleList)
        {
            string[][] surface = new string[sizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[sizeOfGrid].Select(x => FreeSpace).ToArray()
                )
                .ToArray();

            surface = AddObstacles(surface, obstacleList);

            return new MarsSurface(surface);
        }

        private string[][] AddObstacles(string[][] surface, List<Coordinate> obstacleList)
        {
            string[][] updatedSurface = surface;
            
            foreach (var obstacle in obstacleList)
            {
                updatedSurface[obstacle.YCoordinate][obstacle.XCoordinate] = Obstacle;
            }

            return updatedSurface;
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

            return surface;
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