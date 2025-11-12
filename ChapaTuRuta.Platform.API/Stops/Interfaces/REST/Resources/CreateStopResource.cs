namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;

public record CreateStopResource(IFormFile? StopImage, string Name, string Address, Double Latitude, Double Longitude, int DriverId);