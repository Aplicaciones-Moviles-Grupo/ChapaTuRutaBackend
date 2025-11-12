using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;

public class StopRoute
{
    public int Id { get; set; }
    public int RouteId { get; set; }
    public Stop Stop { get; set; }
    public int StopId { get; set; }

    public StopRoute()
    {
        
    }
    public StopRoute(int routeId, Stop stop, int stopId)
    {
        this.RouteId = routeId;
        this.Stop = stop;
        this.StopId = stopId;
    }
    
    public StopRoute(CreateStopRouteCommand command): this(command.RouteId, command.Stop, command.StopId)
    {
        
    }
}