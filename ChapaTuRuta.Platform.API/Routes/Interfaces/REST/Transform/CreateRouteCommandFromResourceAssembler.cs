using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Transform;

public class CreateRouteCommandFromResourceAssembler
{
    public static CreateRouteCommand ToCommandFromResource(CreateRouteResource resource)
    {
        return new CreateRouteCommand(
            resource.Name,
            resource.Price,
            resource.Duration,
            resource.Distance,
            Enum.Parse<RouteState>(resource.State),
            resource.PolylineRoute, 
            resource.DriverId);
    }
}