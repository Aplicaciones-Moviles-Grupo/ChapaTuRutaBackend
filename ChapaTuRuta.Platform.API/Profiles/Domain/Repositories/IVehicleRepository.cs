using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;

public interface IVehicleRepository:IBaseRepository<Vehicle>
{
    Task<Vehicle?> FindByProfileId(int profileId);
}