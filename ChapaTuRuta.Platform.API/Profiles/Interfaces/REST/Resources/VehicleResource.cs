namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

public record VehicleResource(int Id, string Plate, string Model, string Color, string VehicleImageUrl, string VehicleImagePublicId, int ProfileId);