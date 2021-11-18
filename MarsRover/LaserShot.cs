using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class LaserShot
    {
        private Output _output;
        private IMarsSurfaceBuilder _marsSurfaceBuilder;
        private Validations _validations;
        private UtilityMethods _utility;

        public LaserShot(IMarsSurfaceBuilder marsSurfaceBuilder)
        {
            _utility = new UtilityMethods();
            _validations = new Validations();
            _output = new Output();
            _marsSurfaceBuilder = marsSurfaceBuilder;
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
            if (!_validations.LocationIsOnGrid(20, nextSpace))
            {
                return new LaserBeam(coordinate, DisplaySymbol.FreeSpace);
            }
            string symbol = direction is Direction.North or Direction.South 
                ? DisplaySymbol.LaserVertical 
                : DisplaySymbol.LaserHorizontal;

            if (_validations.LocationIsOnGrid(20, nextSpace) && surface.GetPoint(nextSpace) == DisplaySymbol.Obstacle)
            {
                CauseExplosion(surface, nextSpace);
                symbol = DisplaySymbol.FreeSpace;
            }

            return new LaserBeam(nextSpace, symbol);
        }
    }
}