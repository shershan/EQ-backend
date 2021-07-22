using EQ.Models.Models.Identity;

namespace EQ.BLL.Authentication
{
    public interface IAuthenticationService
    {
        UserInRole SignIn(string email, string password);
    }
}
