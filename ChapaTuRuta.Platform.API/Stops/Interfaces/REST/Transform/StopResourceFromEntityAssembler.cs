using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Stops.Interfaces.REST.Transform;

public class StopResourceFromEntityAssembler
{
    public static StopResource ToResourceFromEntity(Stop stop)
    {
        return new StopResource(stop.Id, stop.Name, stop.Address, stop.Latitude, stop.Longitude,stop.StopImageUrl, stop.StopImagePublicId, stop.DriverId);
    }
}