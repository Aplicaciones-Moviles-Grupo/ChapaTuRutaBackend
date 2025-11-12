using ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;

namespace ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

public class Stop
{
    public int Id { get;}
    public string Name { get; set; }
    public string Address { get; set; }
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
    public string StopImageUrl { get; set; }
    public string StopImagePublicId { get; set; }
    public int DriverId { get; set; }

    public Stop(string name, string address, Double latitude, Double longitude,string stopImageUrl,string stopImagePublicId, int driverId)
    {
        this.Name = name;
        this.Address = address;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.StopImageUrl = stopImageUrl;
        this.StopImagePublicId = stopImagePublicId;
        this.DriverId = driverId;
    }

    public Stop(CreateStopCommand command) : this(command.Name, command.Address, command.Latitude, command.Longitude, command.StopImageUrl, command.StopImagePublicId,command.DriverId)
    {
        
    }

    public Stop UpdateInformation(string name, string address, Double latitude, Double longitude, string stopImageUrl,
        string stopImagePublicId)
    {
        this.Name = name;
        this.Address = address;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.StopImageUrl = stopImageUrl;
        this.StopImagePublicId = stopImagePublicId;
        return this;
    }
}