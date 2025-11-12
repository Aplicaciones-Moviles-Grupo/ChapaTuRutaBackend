namespace ChapaTuRuta.Platform.API.Profiles.Interfaces.REST.Resources;

public record UpdateProfileResource(IFormFile? ProfileImage,string FirstName, string LastName, string Email, string PhoneNumber,string ProfileType);