using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.IAM.Domain.Model.Commands;

namespace ChapaTuRuta.Platform.API.IAM.Domain.Services;

public interface IUserCommandService
{
    /// <summary>
    ///     Handle sign in command
    /// </summary>
    /// <param name="command">The sign in command</param>
    /// <returns>The authenticated user and the JWT token</returns>
    Task<(User user, string token)> Handle(SignInCommand command);
    
    /// <summary>
    ///     Handle sign up command
    /// </summary>
    /// <param name="command">The sign-up command</param>
    /// <returns>A confirmation message on successful creation</returns>
    Task<(User user, string token)> Handle(SignUpCommand command);
}