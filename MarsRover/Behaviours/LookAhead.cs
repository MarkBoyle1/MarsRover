using System;

namespace MarsRover.Behaviours
{
    public class LookAhead : IBehaviour
    {
        private Random _random;
        MovementMethods _movement;
        private MarsSurface _surface;
        private int _sizeOfGrid;
        
        public LookAhead(MarsSurface surface)
        {
            _random = new Random();
            _surface = surface;
            _sizeOfGrid = surface.SizeOfGrid;
            _movement = new MovementMethods(surface.SizeOfGrid);
        }

        public RoverLocation ExecuteCommand(RoverLocation location)
        {
            Coordinate nextLocation = _movement.GetNextSpace(location.Coordinate, location.DirectionFacing);
            nextLocation = _movement.WrapAroundPlanetIfRequired(nextLocation);
            string symbol = RevealSpaceInFrontOfRover(_surface, nextLocation);

            return new RoverLocation(nextLocation, location.DirectionFacing, symbol);
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