using Techies.Common.JWTModels;
using Techies.Common.ServiceExtensions.RepositoryPatternExtension;
using Techies.Data;

namespace Techies.Services.UsersRepository
{
    public interface IUserRepository : IRepository<User> { }
}
