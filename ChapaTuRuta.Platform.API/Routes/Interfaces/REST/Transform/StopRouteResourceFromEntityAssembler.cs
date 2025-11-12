using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;

public class StopRouteResourceFromEntityAssembler
{
    public static StopRouteResource ToResourceFromEntity(StopRoute stopRoute)
    {
        return new StopRouteResource(
            stopRoute.Id,
            stopRoute.RouteId,
            stopRoute.Stop);
    }
}