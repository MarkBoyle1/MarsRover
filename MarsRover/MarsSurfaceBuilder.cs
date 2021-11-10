using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class MarsSurfaceBuilder : IMarsSurfaceBuilder
    {
        private const string RoverNorthFacing = "^";
        private const string RoverEastFacing = ">";
        private const string RoverSouthFacing = "v";
        private const string RoverWestFacing = "<";
        private const string Obstacle = "x";
        private const string FreeSpace = ".";
        private const int SizeOfGrid = 20;
        private const int PercentageOfObstacles = 10;
        private List<Coordinate> ObstacleList; 
        private Random random = new Random();

        public MarsSurfaceBuilder(List<Coordinate> obstacleList)
        {
            ObstacleList = obstacleList;
        }

        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[SizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[SizeOfGrid].Select(x => FreeSpace).ToArray()
                )
                .ToArray();

            if (ObstacleList.Count == 0)
            {
                ObstacleList = GenerateRandomObstacles();
            }

            surface = AddObstacles(surface, ObstacleList);

            return new MarsSurface(surface);
        }

        private List<Coordinate> GenerateRandomObstacles()
        {
            List<Coordinate> randomObstacles = new List<Coordinate>();
            int numberOfObstacles = SizeOfGrid * SizeOfGrid / PercentageOfObstacles;

            for (int i = 0; i < numberOfObstacles; i++)
            {
                int xCoordinate = random.Next(0, SizeOfGrid);
                int yCoordinate = random.Next(0, SizeOfGrid);

                Coordinate obstacle = new Coordinate(xCoordinate, yCoordinate);
                
                randomObstacles.Add(obstacle);
            }

            return randomObstacles;
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