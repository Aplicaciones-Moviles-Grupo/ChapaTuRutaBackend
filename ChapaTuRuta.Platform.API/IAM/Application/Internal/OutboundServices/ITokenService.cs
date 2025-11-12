using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<int?> ValidateToken(string token);
}