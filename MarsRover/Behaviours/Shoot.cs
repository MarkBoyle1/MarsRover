using MarsRover.Exceptions;

namespace MarsRover.Behaviours
{
    public class Shoot : IBehaviour
    {
        private MarsSurface _surface;
        private MovementMethods _movement;
        public Shoot(MarsSurface surface)
        {
            _surface = surface;
            _movement = new MovementMethods(surface.SizeOfGrid);
        }
        public ObjectLocation ExecuteCommand(ObjectLocation location)
        {
            Coordinate nextLocation = _movement.GetNextSpace(location.Coordinate, location.DirectionFacing);
            if (!_movement.LocationIsOnGrid(20, nextLocation))
            {
                return new ObjectLocation(nextLocation, location.DirectionFacing,
                    DisplaySymbol.FreeSpace);
            }

            if (_surface.GetPoint(nextLocation) == DisplaySymbol.FreeSpace)
            {
                return new ObjectLocation(nextLocation, location.DirectionFacing,
                    DetermineLaserSymbol(location.DirectionFacing));
            }

            if (_surface.GetPoint(nextLocation) == DisplaySymbol.Obstacle)
            {
                return new ObjectLocation(nextLocation, location.DirectionFacing,
                    DisplaySymbol.Explosion);
            }
            
            return new ObjectLocation(nextLocation, location.DirectionFacing,
                DisplaySymbol.FreeSpace);
        }

        private string DetermineLaserSymbol(Direction direction)
        {
            return direction is Direction.North or Direction.South
                ? DisplaySymbol.LaserVertical
                : DisplaySymbol.LaserHorizontal;
        }

        
    }
}