using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Routes.Infrastructure.Repositories;

public class StopRouteRepository(AppDbContext context):BaseRepository<StopRoute>(context), IStopRouteRepository
{
    public async Task<IEnumerable<StopRoute>> ListByRouteIdAsync(int routeId)
    {
        return await Context.Set<StopRoute>()
            .Where(s => s.RouteId == routeId)
            .ToListAsync();
    }
}