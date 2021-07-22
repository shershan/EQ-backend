using EQ.BLL.BaseDbService;
using EQ.DAL.Models;
using EQ.Helpers.Hash;
using EQ.Models.Models.Identity;
using System;
using System.Linq;

namespace EQ.BLL.Authentication
{
    public class AuthenticationService : BaseDbServices, IAuthenticationService
    {
        public AuthenticationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public UserInRole SignIn(string email, string password)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var user = uow.Repository<User>()
                    .Include(x => x.Role)
                    .Where(x => x.Email == email)
                    .FirstOrDefault();

                if (user != null && !string.IsNullOrEmpty(user.PasswordHash))
                {
                    var passHash = HashHelper.GetPasswordHash(password);
                    if (user.PasswordHash == passHash)
                    {
                        return new UserInRole()
                        {
                            Email = user.Email,
                            UserRole = user.Role.RoleName
                        };
                    }
                }

                return null;
            });
        }
    }
}
