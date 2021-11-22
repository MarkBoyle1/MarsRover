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
        private LaserShot _laserShot;
        private UtilityMethods _utility;

        public Engine(RoverSettings roverSettings, PlanetSettings planetSettings)
        {
            _roverSettings = roverSettings;
            _planetSettings = planetSettings;
            _marsSurfaceBuilder = _planetSettings.MarsSurfaceBuilder;
            _objective = _roverSettings.Objective;
            _utility = new UtilityMethods(_planetSettings.SizeOfGrid);
            _output = new Output(_planetSettings.SizeOfGrid);
            _laserShot = new LaserShot(_marsSurfaceBuilder, _output, _utility, _planetSettings.SizeOfGrid);
            _validations = new Validations();
            _roverBehaviour = new RoverBehaviour(_utility);
            _reportBuilder = new ReportBuilder();
        }
        
        public Report RunProgram()
        {
            RoverLocation roverLocation = _roverSettings.RoverLocation;
            
            MarsSurface surface = _marsSurfaceBuilder.CreateSurface();
            surface = _marsSurfaceBuilder.UpdateSurface(surface, roverLocation.Coordinate, _utility.DetermineDirectionOfRover(roverLocation.DirectionFacing));
            _initialSurface = surface;
            _output.DisplaySurface(surface, 500);

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
            
            return report;
        }
        
        private Report ActivateRover(Report report, Command command)
        {
            RoverLocation newLocation = report.FinalLocation;
            
                if (command.Instruction == RoverInstruction.ShootLaser)
                {
                    MarsSurface surface = _laserShot.FireGun(report.CurrentSurface, report.FinalLocation.Coordinate, report.FinalLocation.DirectionFacing);
                    report = _reportBuilder.CreateReport(_distancedTravelled, _initialSurface, surface, report.FinalLocation);
                }
                else
                {
                    newLocation = _roverBehaviour.ExecuteCommand(report.FinalLocation, command);

                    while (_validations.LocationContainsObstacle(report.CurrentSurface, newLocation))
                    {
                        _output.DisplayMessage(OutputMessages.ObstacleFound);
                        command = _objective.ReceiveCommandForObstacle();
                        if (command.Instruction == RoverInstruction.Stop)
                        {
                            return null;
                        }
                        newLocation = _roverBehaviour.ExecuteCommand(report.FinalLocation, command);
                    }
                    
                    UpdateDistanceTravelled(command);
                    
                    report = MoveRover(report.CurrentSurface, report.FinalLocation, newLocation);
                }
                
                _output.DisplaySurface(report.CurrentSurface, 500);
                
                return report;
        }

        private Report MoveRover(MarsSurface surface, RoverLocation oldLocation, RoverLocation newLocation)
        {
            surface = _marsSurfaceBuilder.UpdateSurface(surface, oldLocation.Coordinate, DisplaySymbol.FreeSpace);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, newLocation.Coordinate,
                _utility.DetermineDirectionOfRover(newLocation.DirectionFacing));
            Coordinate nextLocation =
                _utility.GetNextSpace(newLocation.Coordinate, newLocation.DirectionFacing);
            nextLocation = _utility.WrapAroundPlanetIfRequired(nextLocation);
            surface = _marsSurfaceBuilder.UpdateSurface(surface, nextLocation,
                _utility.RevealSpaceInFrontOfRover(surface, nextLocation));
            
            return _reportBuilder.CreateReport(_distancedTravelled, _initialSurface, surface, newLocation);
        }

        private void UpdateDistanceTravelled(Command command)
        {
            if (command.Instruction is RoverInstruction.MoveForward or RoverInstruction.MoveBack)
            {
                _distancedTravelled += 1;
            }
        }
    }
}