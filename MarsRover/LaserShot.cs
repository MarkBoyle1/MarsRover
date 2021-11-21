using System;

namespace MarsRover
{
    public class LaserShot
    {
        private IOutput _output;
        private IMarsSurfaceBuilder _marsSurfaceBuilder;
        private Validations _validations;
        private UtilityMethods _utility;
        private int _sizeOfGrid;

        public LaserShot(IMarsSurfaceBuilder marsSurfaceBuilder, IOutput output, UtilityMethods utility, int sizeOfGrid)
        {
            _utility = utility;
            _validations = new Validations();
            _output = output;
            _marsSurfaceBuilder = marsSurfaceBuilder;
            _sizeOfGrid = sizeOfGrid;
        }
        
        public MarsSurface FireGun(MarsSurface surface, Coordinate coordinate, Direction direction)
        {
            string symbolForOldLocation =
                _utility.SpaceNeedsToBeCleared(surface, coordinate, _utility.DetermineDirectionOfRover(direction));
            surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, symbolForOldLocation);
            
            LaserBeam laserBeam = UpdateLaserShot(surface, coordinate, direction);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, laserBeam.Coordinate, laserBeam.Symbol);
            
            _output.DisplaySurface(surface, 100);
            
            if (laserBeam.Symbol == DisplaySymbol.FreeSpace)
            {
                return surface;
            }

            return FireGun(surface, laserBeam.Coordinate, direction);
        }

        private void CauseExplosion(MarsSurface surface, Coordinate coordinate)
        {
            Console.Beep();
            string displaySymbol = DisplaySymbol.Explosion;
                  
            surface = _marsSurfaceBuilder.UpdateSurface(surface, coordinate, displaySymbol);
                    
            _output.DisplaySurface(surface, 300);
            _output.DisplaySurface(surface, 300);
            _output.DisplaySurface(surface, 300);
        }

        public LaserBeam UpdateLaserShot(MarsSurface surface, Coordinate coordinate, Direction direction)
        {
            Coordinate nextSpace = _utility.GetNextSpace(coordinate, direction);
            if (!_validations.LocationIsOnGrid(_sizeOfGrid, nextSpace))
            {
                return new LaserBeam(coordinate, DisplaySymbol.FreeSpace);
            }
            string symbol = direction is Direction.North or Direction.South 
                ? DisplaySymbol.LaserVertical 
                : DisplaySymbol.LaserHorizontal;

            if (_validations.LocationIsOnGrid(_sizeOfGrid, nextSpace) && surface.GetPoint(nextSpace) == DisplaySymbol.Obstacle)
            {
                CauseExplosion(surface, nextSpace);
                symbol = DisplaySymbol.FreeSpace;
            }

            return new LaserBeam(nextSpace, symbol);
        }
    }
}