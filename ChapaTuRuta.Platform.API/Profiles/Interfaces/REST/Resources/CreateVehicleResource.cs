namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

public record CreateVehicleResource(IFormFile? VehicleImage,string Plate, string Model, string Color);