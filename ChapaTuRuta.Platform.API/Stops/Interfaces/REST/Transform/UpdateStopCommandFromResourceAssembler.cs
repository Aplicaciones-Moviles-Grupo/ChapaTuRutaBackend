using ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Transform;

public class UpdateStopCommandFromResourceAssembler
{
    public static UpdateStopCommand ToCommandFromResource(UpdateStopResource resource, int id, string stopImageUrl, string stopImagePublicId)
    {
        return new UpdateStopCommand(id, resource.Name, resource.Address, resource.Latitude, resource.Longitude, stopImageUrl, stopImagePublicId);
    }
}