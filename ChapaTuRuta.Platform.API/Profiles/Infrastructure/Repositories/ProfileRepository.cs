using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapaTuRuta.Platform.API.Profiles.Infrastructure.Repositories;

public class ProfileRepository(AppDbContext context): BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<IEnumerable<Profile>> ListByProfileTypeAsync(ProfileType type)
    {
        return await Context.Set<Profile>().Where(p => p.Type == type).ToListAsync();
    }

    public async Task<Profile?> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.UserId == userId);
    }
}