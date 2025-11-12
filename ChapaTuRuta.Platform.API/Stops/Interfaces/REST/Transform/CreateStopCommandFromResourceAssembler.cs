using ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Transform;

public class CreateStopCommandFromResourceAssembler
{
    public static CreateStopCommand ToCommandFromResource(CreateStopResource resource, string stopImageUrl,string stopImagePublicId)
    {
        return new CreateStopCommand(resource.Name, resource.Address, resource.Latitude, resource.Longitude, stopImageUrl, stopImagePublicId,resource.DriverId);
    }
}