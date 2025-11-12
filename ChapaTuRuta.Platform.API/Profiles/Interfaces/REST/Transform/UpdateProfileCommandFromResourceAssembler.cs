using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;

public class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(UpdateProfileResource resource, int id, string profileImageUrl, string profileImagePublicId)
    {
        return new UpdateProfileCommand(
            id,
            resource.FirstName, 
            resource.LastName, 
            resource.Email, 
            resource.PhoneNumber, 
            Enum.Parse<ProfileType>(resource.ProfileType), 
            profileImageUrl, 
            profileImagePublicId);
    }
}