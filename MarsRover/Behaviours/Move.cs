namespace MarsRover.Behaviours
{
    public class Move : IBehaviour
    {
        private MovementMethods _movement;
        private RoverInstruction _instruction;

        public Move(RoverInstruction instruction)
        {
            _instruction = instruction;
            _movement = new MovementMethods(20);
        }

        public ObjectLocation ExecuteCommand(ObjectLocation location)
        {
            Coordinate newCoordinate = location.Coordinate;
            Direction directionFacing = location.DirectionFacing;
            
            newCoordinate = MoveRover(location.Coordinate, directionFacing, _instruction);
            newCoordinate = _movement.WrapAroundPlanetIfRequired(newCoordinate);
            
            return new ObjectLocation(newCoordinate, directionFacing, location.Symbol);
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

            return newCoordinate;
        }
    }
}