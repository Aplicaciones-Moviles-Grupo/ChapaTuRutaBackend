using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Repositories;

public interface IStopRouteRepository:IBaseRepository<StopRoute>
{
    Task<IEnumerable<StopRoute>> ListByRouteIdAsync(int routeId);
}