namespace ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;

public record UpdateStopCommand(int Id, string Name, string Address, Double Latitude, Double Longitude, string StopImageUrl, string StopImagePublicId);