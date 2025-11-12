namespace ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;

public record UpdateVehicleCommand(int Id, string Model, string Plate, string Color, string VehicleImageUrl, string VehicleImagePublicId);