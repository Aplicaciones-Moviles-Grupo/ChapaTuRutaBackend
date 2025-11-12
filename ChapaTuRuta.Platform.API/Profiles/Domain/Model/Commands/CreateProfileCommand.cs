using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(string FirstName, string LastName,string Email, string PhoneNumber,ProfileType Type, string ProfileImageUrl,string ProfileImagePublicId, int UserId);