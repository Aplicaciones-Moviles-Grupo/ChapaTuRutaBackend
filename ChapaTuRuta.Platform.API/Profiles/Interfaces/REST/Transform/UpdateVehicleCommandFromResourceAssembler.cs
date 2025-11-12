using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;

public class UpdateVehicleCommandFromResourceAssembler
{
    public static UpdateVehicleCommand ToCommandFromResource(UpdateVehicleResource resource, int id, string vehicleImageUrl, string vehicleImagePublicId)
    {
        return new UpdateVehicleCommand(id, resource.Model, resource.Color, resource.Plate, vehicleImageUrl, vehicleImagePublicId);
    }
}