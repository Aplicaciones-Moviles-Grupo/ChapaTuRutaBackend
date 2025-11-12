using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;

namespace ChapaTuRuta.Platform.API.Profiles.Application.Internal.QueryServices;

public class VehicleQueryService(IVehicleRepository vehicleRepository):IVehicleQueryService
{
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query)
    {
        return await vehicleRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<Vehicle?> Handle(GetVehicleByProfileIdQuery query)
    {
        return await vehicleRepository.FindByProfileId(query.ProfileId);
    }
}