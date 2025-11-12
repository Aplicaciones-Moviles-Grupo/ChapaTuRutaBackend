using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;

public interface IProfileRepository:IBaseRepository<Profile>
{
    Task<IEnumerable<Profile>> ListByProfileTypeAsync(ProfileType type);
    Task<Profile?> FindByUserIdAsync(int userId);
}