namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;

public record StopResource(int Id, string Name, string Address, Double Latitude, Double Longitude,string StopImageUrl, string StopImagePublicId, int DriverId);