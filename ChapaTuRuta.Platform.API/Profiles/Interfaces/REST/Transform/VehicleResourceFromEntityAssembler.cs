using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;

public class VehicleResourceFromEntityAssembler
{
    public static VehicleResource ToResourceFromEntity(Vehicle vehicle)
    {
        return new VehicleResource(vehicle.Id,vehicle.Plate,vehicle.Model,vehicle.Color,vehicle.VehicleImageUrl,vehicle.VehicleImagePublicId,vehicle.ProfileId);
    }
}