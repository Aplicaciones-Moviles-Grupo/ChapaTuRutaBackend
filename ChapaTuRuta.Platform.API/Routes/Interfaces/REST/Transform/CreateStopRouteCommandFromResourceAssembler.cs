using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;

public class CreateStopRouteCommandFromResourceAssembler
{
    public static CreateStopRouteCommand ToCommandFromResource(CreateStopRouteResource resource, int routeId, Stop stop)
    {
        return new CreateStopRouteCommand(
            routeId,
            resource.StopId,
            stop);
    }
}