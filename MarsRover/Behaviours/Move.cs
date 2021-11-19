namespace MarsRover.Behaviours
{
    public class Move : IBehaviour
    {
        private UtilityMethods _utility;
        private RoverInstruction _instruction;
        private int sizeOfGrid = 20;

        public Move(RoverInstruction instruction, UtilityMethods utility)
        {
            _instruction = instruction;
            _utility = utility;
        }

        public RoverLocation ExecuteCommand(RoverLocation location)
        {
            Coordinate newCoordinate = location.Coordinate;
            Direction directionFacing = location.DirectionFacing;
            
            newCoordinate = MoveRover(location.Coordinate, directionFacing, _instruction);
            
            return new RoverLocation(newCoordinate, directionFacing);
        }
        
        private Coordinate MoveRover(Coordinate coordinate, Direction currentDirection, RoverInstruction instruction)
        {
            int xCoordinate = coordinate.XCoordinate;
            int yCoordinate = coordinate.YCoordinate;

            switch (currentDirection)
            {
                case Direction.North:
                    yCoordinate = instruction == RoverInstruction.MoveForward ? yCoordinate - 1 : yCoordinate + 1;
                    break;
                case Direction.East:
                    xCoordinate = instruction == RoverInstruction.MoveForward ? xCoordinate + 1 : xCoordinate - 1;
                    break;
                case Direction.South:
                    yCoordinate = instruction == RoverInstruction.MoveForward ? yCoordinate + 1 : yCoordinate - 1;
                    break;
                case Direction.West:
                    xCoordinate = instruction == RoverInstruction.MoveForward ? xCoordinate - 1 : xCoordinate + 1;
                    break;
            }
            
            Coordinate newCoordinate = new Coordinate(xCoordinate, yCoordinate);

            return _utility.WrapAroundPlanetIfRequired(newCoordinate);
        }
    }
}