using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;

public class Vehicle
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
    public string Color { get; set; }
    public int ProfileId { get; set; }
    public string VehicleImageUrl { get; set; }
    public string VehicleImagePublicId { get; set; }

    public Vehicle(string model, string plate, string color, string vehicleImageUrl, string vehicleImagePublicId, int profileId)
    {
        Model = model;
        Plate = plate;
        Color = color;
        ProfileId = profileId;
        VehicleImageUrl = vehicleImageUrl;
        VehicleImagePublicId = vehicleImagePublicId;
    }

    public Vehicle(CreateVehicleCommand command): this(command.Model,command.Plate, command.Color, command.VehicleImageUrl, command.VehicleImagePublicId, command.ProfileId)
    {
        
    }

    public Vehicle UpdateInformation(string model, string plate, string color, string vehicleImageUrl,
        string vehicleImagePublicId)
    {
        Model = model;
        Plate = plate;
        Color = color;
        VehicleImageUrl = vehicleImageUrl;
        VehicleImagePublicId = vehicleImagePublicId;
        return this;
    }
}