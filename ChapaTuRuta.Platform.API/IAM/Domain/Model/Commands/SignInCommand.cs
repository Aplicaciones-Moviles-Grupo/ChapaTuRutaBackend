namespace ChapaTuRuta.Platform.API.IAM.Domain.Model.Commands;

/// <summary>
///     The sign in command
/// </summary>
/// <param name="Email"> The email of user aggregate</param>
/// <param name="Password">The password of user aggregate</param>
public record SignInCommand(string Email, string Password);