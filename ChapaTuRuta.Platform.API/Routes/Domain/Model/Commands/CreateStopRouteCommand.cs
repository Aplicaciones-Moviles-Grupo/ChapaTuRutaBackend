using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;

public record CreateStopRouteCommand(int RouteId, int StopId, Stop Stop);