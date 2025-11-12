using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.ValueObjects;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;

public class TravelRoute
{
    public int Id { get; }
    public String Name { get; set; }
    public Double Price { get; set; }
    public String Duration { get; set; }
    public String Distance { get; set; }
    public RouteState State { get; set; }
    
    public String PolylineRoute { get; set; }
    
    public int DriverId { get; set; }
    
    public TravelRoute(String name, Double price, String duration, String distance, RouteState state, String polylineRoute, int driverId)
    {
        this.Name = name;
        this.Price = price;
        this.Duration = duration;
        this.Distance = distance;
        this.State = state;
        this.PolylineRoute = polylineRoute;
        this.DriverId = driverId;
    }
    
    public TravelRoute(CreateRouteCommand command): this(command.Name, command.Price, command.Duration, command.Distance, command.State, command.PolylineRoute,command.DriverId)
    {
        
    }
    
    public TravelRoute UpdateInformation(String name, Double price, String duration, String distance, RouteState state)
    {
        this.Name = name;
        this.Price = price;
        this.Duration = duration;
        this.Distance = distance;
        this.State = state;
        return this;
    }
    
    public void Activate()
    {
        this.State = RouteState.Active;
    }
    
    public void Desactivate()
    {
        this.State = RouteState.Inactive;
    }
}