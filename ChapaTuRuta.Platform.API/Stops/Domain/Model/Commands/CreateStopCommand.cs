namespace ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;

public record CreateStopCommand(string Name, string Address, Double Latitude, Double Longitude, string StopImageUrl, string StopImagePublicId,int DriverId);