using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;

namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<int> CreateProfile(string firstName, string lastName, string email, string phoneNumber, string newType, string profileImageUrl, string profileImagePublicId, int userId);
}