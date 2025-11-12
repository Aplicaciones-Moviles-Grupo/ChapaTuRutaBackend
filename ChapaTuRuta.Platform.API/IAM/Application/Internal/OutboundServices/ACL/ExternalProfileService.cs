using ChapaTuRuta.Platform.API.IAM.Interfaces.ACL;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.ACL;

namespace ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices.ACL;

public class ExternalProfileService: IExternalProfileService
{
    private readonly IProfilesContextFacade _profilesContextFacade;

    public ExternalProfileService(IProfilesContextFacade profilesContextFacade)
    {
        _profilesContextFacade = profilesContextFacade;
    }

    public async Task<int> CreateProfile(string firstName, string lastName, string email, string phoneNumber, string newType,
        string profileImageUrl, string profileImagePublicId, int userId)
    {
        var profileId = await _profilesContextFacade.CreateProfile(firstName, lastName, email, phoneNumber, newType,
            profileImageUrl, profileImagePublicId, userId);
        
        return profileId;
    }
}