using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Stops.Domain.Repositories;

public interface IStopRepository:IBaseRepository<Stop>
{
    Task<IEnumerable<Stop>> ListByDriverIdAsync(int driverId);
}