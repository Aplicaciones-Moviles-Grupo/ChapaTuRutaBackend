using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Profiles.Domain.Repositories;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;

namespace ChapaTuRuta.Platform.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository): IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        return await profileRepository.FindByUserIdAsync(query.UserId);
    }
    
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllDriverProfilesQuery query)
    {
        return await profileRepository.ListByProfileTypeAsync(query.Type);
    }
}