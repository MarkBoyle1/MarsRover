using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class MarsSurfaceBuilder : IMarsSurfaceBuilder
    {
        private int SizeOfGrid = 20;
        private const int PercentageOfObstacles = 10;
        private int AreasDiscovered;
        private List<Coordinate> ObstacleList; 
        private Random random = new Random();

        public MarsSurfaceBuilder(List<Coordinate> obstacleList, int sizeOfGrid)
        {
            ObstacleList = obstacleList;
            SizeOfGrid = sizeOfGrid;
            AreasDiscovered = SizeOfGrid * SizeOfGrid;
        }

        public MarsSurface CreateSurface()
        {
            string[][] surface = new string[SizeOfGrid][];
            surface = surface.Select
                (
                    x => new string[SizeOfGrid].Select(x => DisplaySymbol.FreeSpace).ToArray()
                )
                .ToArray();

            if (ObstacleList.Count == 0)
            {
                ObstacleList = GenerateRandomObstacles();
            }

            surface = AddObstacles(surface, ObstacleList);

            return new MarsSurface(surface, ObstacleList.Count, AreasDiscovered);
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
                updatedSurface[obstacle.YCoordinate][obstacle.XCoordinate] = DisplaySymbol.Obstacle;
            }

            return updatedSurface;
        }
        
        public MarsSurface UpdateSurface(MarsSurface surface, Coordinate location, string symbol)
        {
            string[][] updatedSurface = new string[SizeOfGrid][];
            int obstacleCount = symbol == DisplaySymbol.Obstacle ? 1 : 0;
            updatedSurface = updatedSurface.Select(x => new string[SizeOfGrid]).ToArray();
                
            for(int x = 0; x < SizeOfGrid; x++)
            {
                for (int y = 0; y < SizeOfGrid; y++)
                {
                    string surfacePoint = surface.GetPoint(new Coordinate(x,y));
                    updatedSurface[y][x] = surfacePoint;
                    obstacleCount = surfacePoint == DisplaySymbol.Obstacle ? obstacleCount + 1 : obstacleCount;
                }
            }
            obstacleCount = surface.Surface[location.YCoordinate][location.XCoordinate] == DisplaySymbol.Obstacle ? obstacleCount - 1 : obstacleCount;
            updatedSurface[location.YCoordinate][location.XCoordinate] = symbol;
            
            return new MarsSurface(updatedSurface, obstacleCount, AreasDiscovered);
        }
    }
}