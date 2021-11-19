using System;

namespace MarsRover
{
    public class UtilityMethods
    {
        private int _sizeOfGrid;
        private Random _random = new Random();

        public UtilityMethods(int sizeOfGrid)
        {
            _sizeOfGrid = sizeOfGrid;
        }
        public Coordinate GetNextSpace(Coordinate coordinate, Direction direction)
        {
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
        
        public string DetermineDirectionOfRover(Direction direction)
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
        
        public string SpaceNeedsToBeCleared(MarsSurface surface, Coordinate coordinate, string roverImage)
        {
            return surface.Surface[coordinate.YCoordinate][coordinate.XCoordinate] == roverImage
                ? roverImage
                : DisplaySymbol.FreeSpace;
        }
        
        public Coordinate WrapAroundPlanetIfRequired(Coordinate coordinate)
        {
            int xCoordinate = AdjustIndividualCoordinate(coordinate.XCoordinate);
            int yCoordinate = AdjustIndividualCoordinate(coordinate.YCoordinate);
        
            return new Coordinate(xCoordinate, yCoordinate);
        }

        public int AdjustIndividualCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                return _sizeOfGrid - 1;
            }
            
            if (coordinate > _sizeOfGrid - 1)
            {
                return 0;
            }
            
            return coordinate;
        }
        
        public string RevealSpaceInFrontOfRover(MarsSurface surface, Coordinate coordinate)
        {
            string revealedSpace = surface.Surface[coordinate.YCoordinate][coordinate.XCoordinate];
            if (revealedSpace == DisplaySymbol.UnknownSpace)
            {
                int randomNumber = _random.Next(1, 11);
                return randomNumber > 2 ? DisplaySymbol.FreeSpace : DisplaySymbol.Obstacle;
            }
            
            return revealedSpace;
        }
    }
}