using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Profiles.Infrastructure.Repositories;

public class VehicleRepository(AppDbContext context):BaseRepository<Vehicle>(context), IVehicleRepository
{
    public async Task<Vehicle?> FindByProfileId(int profileId)
    {
        return await Context.Set<Vehicle>().FirstOrDefaultAsync(v => v.ProfileId == profileId);
    }
}