using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;

public record StopRouteResource(int Id, int RouteId, Stop Stop);