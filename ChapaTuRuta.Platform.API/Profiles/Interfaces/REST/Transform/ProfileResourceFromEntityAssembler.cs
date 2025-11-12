using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;

public class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile profile)
    {
        return new ProfileResource(
            profile.Id, 
            profile.FirstName, 
            profile.LastName, 
            profile.Email, 
            profile.PhoneNumber,
            profile.Type.GetDisplayName(), 
            profile.ProfileImageUrl, 
            profile.ProfileImagePublicId, 
            profile.UserId);
    }
}