namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

public record UpdateVehicleResource(IFormFile? VehicleImage, string Plate, string Model, string Color);