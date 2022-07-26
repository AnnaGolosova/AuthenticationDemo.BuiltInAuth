using Demo.AuthService.Models;

namespace Demo.AuthService.Repositories.Abstract
{
    public interface IJWTRepository
    {
        Tokens Authenticate(User user);
    }
}
