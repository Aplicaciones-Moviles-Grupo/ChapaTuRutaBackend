using ChapaTuRuta.Platform.API.IAM.Application.Internal.OutboundServices;
using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.IAM.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.IAM.Domain.Repositories;
using ChapaTuRuta.Platform.API.IAM.Domain.Services;
using ChapaTuRuta.Platform.API.IAM.Interfaces.ACL;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.IAM.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, ITokenService tokenService, IHashingService hashingService, IExternalProfileService externalProfileService,IUnitOfWork unitOfWork): IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.Email);
        if(user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
             
            throw new Exception($"Invalid username or password");
        
        var token = tokenService.GenerateToken(user);
        return (user,token);
    }

    public async Task<(User user, string token)> Handle(SignUpCommand command)
    {
        if(userRepository.ExistsByEmail(command.Email))
            throw new Exception($"User with email {command.Email} already exists");
        
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Email, hashedPassword);
        var token = tokenService.GenerateToken(user);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            //External Create Profile
            await externalProfileService.CreateProfile("","",user.Email,"","None","","",user.Id);
            return (user,token);
        }
        catch (Exception e)
        {
            
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}