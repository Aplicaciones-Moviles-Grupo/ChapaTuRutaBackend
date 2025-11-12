using ChapaTuRuta.Platform.API.Routes.Domain.Model.ValueObjects;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;

public record CreateRouteCommand(String Name, Double Price, String Duration, String Distance, RouteState State, String PolylineRoute, int DriverId);