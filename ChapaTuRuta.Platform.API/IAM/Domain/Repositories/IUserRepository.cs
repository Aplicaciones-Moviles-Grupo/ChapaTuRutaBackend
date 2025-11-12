using ChapaTuRuta.Platform.API.IAM.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    
    bool ExistsByEmail(string email);
}