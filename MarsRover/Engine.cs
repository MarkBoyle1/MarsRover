using MarsRover.Exceptions;
using MarsRover.Objectives;

namespace MarsRover
{
    public class Engine
    {
        private IMarsSurfaceBuilder _marsSurfaceBuilder;
        private ReportBuilder _reportBuilder;
        private RoverBehaviour _roverBehaviour;
        private Validations _validations;
        private IOutput _output;
        private IObjective _objective;
        private RoverSettings _roverSettings;
        private PlanetSettings _planetSettings;
        private MarsSurface _initialSurface;
        private int _distancedTravelled;
        private int RoverSpeed = 500;
        private int ExplosionSpeed = 300;
        private int LaserSpeed= 200;

        public Engine(RoverSettings roverSettings, PlanetSettings planetSettings)
        {
            _roverSettings = roverSettings;
            _planetSettings = planetSettings;
            _marsSurfaceBuilder = _planetSettings.MarsSurfaceBuilder;
            _objective = _roverSettings.Objective;
            _output = new Output(_planetSettings.SizeOfGrid);
            _validations = new Validations(_planetSettings.SizeOfGrid);
            _roverBehaviour = new RoverBehaviour();
            _reportBuilder = new ReportBuilder();
        }
        
        public Report RunProgram()
        {
            RoverLocation roverLocation = _roverSettings.RoverLocation;
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverLocation.Coordinate, roverLocation.Symbol);
            _initialSurface = surface;
            _output.DisplaySurface(surface, RoverSpeed);

            Report report = _reportBuilder.CreateReport(_distancedTravelled, surface, surface, roverLocation);
            
            Command command = _objective.ReceiveCommand();
            
            while (command.Instruction != RoverInstruction.Stop && !_objective.CheckForCompletion(report))
            {
                Report oldReport = report;
                report = ActivateRover(report, command);

                if (report == null)
                {
                    report = oldReport;
                    break;
                }
                
                command = _objective.ReceiveCommand();
            }
            
            _output.DisplayReport(report);
            _output.CreateReportFile(report);
            
            return report;
        }
        
        private Report ActivateRover(Report report, Command command)
        {
            RoverLocation newLocation = report.FinalLocation;
            
                if (command.Instruction == RoverInstruction.ShootLaser)
                {
                    report = FireLaser(report);
                }
                else
                {
                    newLocation = _roverBehaviour.ExecuteCommand(report.FinalLocation, command, report.CurrentSurface);

                    while (_validations.LocationContainsObstacle(report.CurrentSurface, newLocation))
                    {
                        _output.DisplayMessage(OutputMessages.ObstacleFound);
                        command = _objective.ReceiveCommandForObstacle();
                        if (command.Instruction == RoverInstruction.Stop)
                        {
                            return null;
                        }
                        newLocation = _roverBehaviour.ExecuteCommand(report.FinalLocation, command, report.CurrentSurface);
                    }
                    
                    UpdateDistanceTravelled(command);
                    
                    report = MoveRover(report.CurrentSurface, report.FinalLocation, newLocation);
                }
                
                _output.DisplaySurface(report.CurrentSurface, RoverSpeed);
                
                return report;
        }

        private Report MoveRover(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation)
        {
            //Update old Location
            surface = _marsSurfaceBuilder.UpdateSurface(surface, oldLocation.Coordinate, DisplaySymbol.FreeSpace);
            
            //Update new Location
            surface = _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate,
                newLocation.Symbol);
            
            //Reveal next space
            RoverLocation nextLocation =
                _roverBehaviour.ExecuteCommand(newLocation, new Command(RoverInstruction.LookAhead), surface);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, nextLocation.Coordinate,
                nextLocation.Symbol);
            
            return _reportBuilder.CreateReport(_distancedTravelled, _initialSurface, surface, newLocation);
        }

        private void UpdateDistanceTravelled(Command command)
        {
            if (command.Instruction is RoverInstruction.MoveForward or RoverInstruction.MoveBack)
            {
                _distancedTravelled += 1;
            }
        }

        private Report FireLaser(Report report)
        {
            MarsSurface surface = report.CurrentSurface;
            string roverImage= report.FinalLocation.Symbol;
            RoverLocation newLocation = report.FinalLocation;

            while (_validations.LocationIsOnGrid(20, newLocation.Coordinate))
            {
                surface =
                    _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate, SpaceNeedsToBeCleared(surface, newLocation.Coordinate, roverImage));
            
                newLocation =
                    _roverBehaviour.ExecuteCommand(newLocation, new Command(RoverInstruction.ShootLaser), surface);

                if (!_validations.LocationIsOnGrid(20, newLocation.Coordinate))
                {
                    break;
                }

                surface =
                    _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate, newLocation.Symbol);
                
                if (newLocation.Symbol == DisplaySymbol.Explosion)
                {
                    _output.DisplaySurface(surface, ExplosionSpeed);
                    _output.DisplaySurface(surface, ExplosionSpeed);
                    _output.DisplaySurface(surface, ExplosionSpeed);
                    surface =
                        _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate, DisplaySymbol.FreeSpace);
                    return _reportBuilder.CreateReport(report.DistanceTravelled, _initialSurface, surface,
                        report.FinalLocation);
                }
                
                _output.DisplaySurface(surface, LaserSpeed);
            }
            
            return _reportBuilder.CreateReport(report.DistanceTravelled, _initialSurface, surface,
                report.FinalLocation);
        }
        
        private string SpaceNeedsToBeCleared(MarsSurface surface, Coordinate coordinate, string roverImage)
        {
            return surface.Surface[coordinate.YCoordinate][coordinate.XCoordinate] == roverImage
                ? roverImage
                : DisplaySymbol.FreeSpace;
        }
    }
}