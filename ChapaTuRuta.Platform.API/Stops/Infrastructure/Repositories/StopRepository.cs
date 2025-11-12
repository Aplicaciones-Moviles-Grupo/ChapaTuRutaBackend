using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Stops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Stops.Infrastructure.Repositories;

public class StopRepository(AppDbContext context): BaseRepository<Stop>(context), IStopRepository
{
    public async Task<IEnumerable<Stop>> ListByDriverIdAsync(int driverId)
    {
        return await Context.Set<Stop>().Where(s => s.DriverId == driverId).ToListAsync();
    }
}