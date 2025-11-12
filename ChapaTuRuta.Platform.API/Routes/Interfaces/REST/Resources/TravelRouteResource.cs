namespace ChapaTuRuta.Platform.API.Routes.Interfaces.REST.Resources;

public record TravelRouteResource(int Id, String Name, Double Price, 
    String Duration, String Distance, 
    String State, String PolylineRoute, int DriverId);