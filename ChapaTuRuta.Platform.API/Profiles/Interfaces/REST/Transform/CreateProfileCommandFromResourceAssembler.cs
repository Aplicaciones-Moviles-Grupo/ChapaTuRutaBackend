using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;
using ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Transform;

public class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource, string profileImageUrl, string profileImagePublicId)
    {
        return new CreateProfileCommand(resource.FirstName, resource.LastName, resource.Email, resource.PhoneNumber,Enum.Parse<ProfileType>(resource.ProfileType), profileImageUrl,profileImagePublicId, resource.UserId);
    }
}