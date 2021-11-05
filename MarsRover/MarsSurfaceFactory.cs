using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class MarsSurfaceFactory
    {
        public const string RoverNorthFacing = "^";
        public const string RoverEastFacing = ">";
        public const string RoverSouthFacing = "v";
        public const string RoverWestFacing = "<";
        public MarsSurface CreateSurface(int sizeOfGrid, List<Coordinate> obstacleList)
        {
            string[][] surface = new string[sizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[sizeOfGrid].Select(x => ".").ToArray()
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
                updatedSurface[obstacle.XCoordinate][obstacle.YCoordinate] = "x";
            }

            return updatedSurface;
        }
        
        public MarsSurface PlaceRoverOnStartingPoint(MarsSurface surface, RoverLocation startingPoint)
        {
            string rover = "";

            switch (startingPoint.DirectionFacing)
            {
                case Direction.North:
                    rover = RoverNorthFacing;
                    break;
                case Direction.East:
                    rover = RoverEastFacing;
                    break;
                case Direction.South:
                    rover = RoverSouthFacing;
                    break;
                case Direction.West:
                    rover = RoverWestFacing;
                    break;
            }

            surface.Surface[startingPoint.Coordinate.XCoordinate][startingPoint.Coordinate.YCoordinate] = rover;

            return surface;
        }
    }
}