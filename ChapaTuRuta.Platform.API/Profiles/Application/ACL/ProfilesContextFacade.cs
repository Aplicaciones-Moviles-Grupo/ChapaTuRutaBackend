using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Profiles.Domain.Services;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.ACL;

namespace ChapaTuRuta.Platform.API.Profiles.Application.ACL;

public class ProfilesContextFacade(IProfileCommandService profileCommandService):IProfilesContextFacade
{
    public async Task<int> CreateProfile(string firstName, string lastName, string email, string phoneNumber, string newType,
        string profileImageUrl, string profileImagePublicId, int userId)
    {
        var createProfileCommand = new CreateProfileCommand(firstName, lastName, email, phoneNumber, Enum.Parse<ProfileType>(newType), profileImageUrl, profileImagePublicId, userId);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id ?? 0;
    }
}