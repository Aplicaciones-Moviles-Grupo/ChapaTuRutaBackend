using ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;


namespace ChapaTuRuta.Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;

public class HashingService:IHashingService
{
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password,passwordHash);
    }
}