namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

public record ProfileResource(int Id, string FirstName, string LastName, string Email, string PhoneNumber,string ProfileType,string ProfileImageUrl, string ProfileImagePublicId, int UserId);