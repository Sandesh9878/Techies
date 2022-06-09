using Techies.Common.JWTModels;
using Techies.Common.ServiceExtensions.RepositoryPatternExtension;
using Techies.Data;

namespace Techies.Services.UsersRepository
{
    public class UsersRepo : EfCoreRepository<User>, IUserRepository
    {
        public UsersRepo(TechiesContext context) : base(context) { }
    }
}
