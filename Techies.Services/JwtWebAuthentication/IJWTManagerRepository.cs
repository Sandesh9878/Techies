using Techies.Common.JWTModels;
using Techies.Common.UsersDto;

namespace Techies.Services.JwtWebAuthentication
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
