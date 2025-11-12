using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Services;

public interface IProfileQueryService
{
    Task<Profile?> Handle(GetProfileByUserIdQuery query);
    Task<IEnumerable<Profile>> Handle(GetAllDriverProfilesQuery query);
    Task<Profile?> Handle(GetProfileByIdQuery query);
}