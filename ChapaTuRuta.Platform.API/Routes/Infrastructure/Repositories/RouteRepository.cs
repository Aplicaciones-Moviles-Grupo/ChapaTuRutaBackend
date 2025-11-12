using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Routes.Infrastructure.Repositories;

public class RouteRepository(AppDbContext context):BaseRepository<TravelRoute>(context), IRouteRepository
{
    public async Task<IEnumerable<TravelRoute>> ListByDriverIdAsync(int driverId)
    {
        return await Context.Set<TravelRoute>()
            .Where(r => r.DriverId == driverId)
            .ToListAsync();
    }
}