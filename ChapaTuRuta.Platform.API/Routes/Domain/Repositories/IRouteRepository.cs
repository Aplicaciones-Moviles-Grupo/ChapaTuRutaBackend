using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Repositories;

public interface IRouteRepository:IBaseRepository<TravelRoute>
{
    Task<IEnumerable<TravelRoute>> ListByDriverIdAsync(int driverId);
}