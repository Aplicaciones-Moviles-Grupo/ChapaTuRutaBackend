using ChapaTuRuta.Platform.API.IAM.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Email, resource.Password);
    }
}