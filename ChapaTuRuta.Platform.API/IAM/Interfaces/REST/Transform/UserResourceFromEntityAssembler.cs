using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Resources;

namespace ChapaTuRuta.Platform.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Email);
    }
}