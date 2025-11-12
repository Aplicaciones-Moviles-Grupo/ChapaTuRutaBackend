using ChapaTuRuta.Platform.API.IAM.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}