namespace ChapaTuRuta.Platform.API.IAM.Interfaces.ACL;

public interface IExternalProfileService
{
    Task<int> CreateProfile(string firstName, string lastName, string email, string phoneNumber,
        string newType, string profileImageUrl, string profileImagePublicId, int userId);
}