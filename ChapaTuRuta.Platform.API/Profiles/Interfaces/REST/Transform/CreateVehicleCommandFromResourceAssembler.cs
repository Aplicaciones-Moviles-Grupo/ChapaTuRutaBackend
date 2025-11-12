using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;

public class CreateVehicleCommandFromResourceAssembler
{
    public static CreateVehicleCommand ToCommandFromResource(CreateVehicleResource resource, string vehicleImageUrl, string vehicleImagePublicId, int profileId)
    {
        return new CreateVehicleCommand(resource.Model, resource.Plate, resource.Color, vehicleImageUrl, vehicleImagePublicId, profileId);
    }
}