using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;

public class TravelRouteResourceFromEntityAssembler
{
    public static TravelRouteResource ToResourceFromEntity(TravelRoute travelRoute)
    {
        return new TravelRouteResource(
            travelRoute.Id,
            travelRoute.Name,
            travelRoute.Price,
            travelRoute.Duration,
            travelRoute.Distance,
            travelRoute.State.GetDisplayName(),
            travelRoute.PolylineRoute,
            travelRoute.DriverId);
    }
}