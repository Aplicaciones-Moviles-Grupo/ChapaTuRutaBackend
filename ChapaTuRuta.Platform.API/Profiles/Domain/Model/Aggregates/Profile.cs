using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.ValueObjects;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;

public class Profile
{
    public int Id { get;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfileImageUrl { get; set; }
    public string ProfileImagePublicId { get; set; }
    public ProfileType Type { get; set; }
    public int UserId { get; set; }

    public Profile(string firstName, string lastName, string email, string phoneNumber,ProfileType type, string profileImageUrl, string profileImagePublicId, int userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        ProfileImageUrl = profileImageUrl;
        ProfileImagePublicId = profileImagePublicId;
        UserId = userId;
        Type = type;
    }

    public Profile(CreateProfileCommand command): this(command.FirstName, command.LastName, command.Email, command.PhoneNumber,command.Type, command.ProfileImageUrl, command.ProfileImagePublicId, command.UserId)
    {
        if (String.IsNullOrEmpty(ProfileImageUrl))
        {
            ProfileImageUrl = "https://res.cloudinary.com/dtxv5wnbj/image/upload/v1762385933/default-profile_kxt5l2.jpg";
        }

        if (String.IsNullOrEmpty(ProfileImagePublicId))
        {
            ProfileImagePublicId = "default-profile_kxt5l2";
        }
    }

    public Profile UpdateInformation(string firstName, string lastName, string email, string phoneNumber, ProfileType newType, string profileImageUrl, string profileImagePublicId)
    {
        if (Type != ProfileType.None && Type != newType)
        {
            throw new ArgumentException("Cannot change the type of profile if it is Driver or Client.");
        }

        if (Type == ProfileType.None && newType == ProfileType.None)
        {
            throw new ArgumentException("You must select a type of profile (Driver or Client).");
        }
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        ProfileImageUrl = profileImageUrl;
        ProfileImagePublicId = profileImagePublicId;
        Type = newType;
        return this;
    }
}