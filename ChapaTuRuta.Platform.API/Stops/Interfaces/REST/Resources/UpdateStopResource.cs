namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;

public record UpdateStopResource(IFormFile? StopImage, string Name, string Address, Double Latitude, Double Longitude);