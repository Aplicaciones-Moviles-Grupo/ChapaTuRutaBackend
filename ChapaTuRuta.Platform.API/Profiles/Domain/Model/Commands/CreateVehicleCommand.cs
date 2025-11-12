namespace ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;

public record CreateVehicleCommand(string Model, string Plate, string Color, string VehicleImageUrl, string VehicleImagePublicId, int ProfileId);