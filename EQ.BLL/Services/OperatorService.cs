using EQ.BLL.Abstractions;
using EQ.BLL.BaseDbService;
using EQ.Constants;
using EQ.DAL.Models;
using EQ.Helpers.Hash;
using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EQ.BLL.Services
{
    internal class OperatorService : BaseDbServices, IOperatorService
    {
        public OperatorService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public OperatorModel CreateOperator(string email, string password)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var existedUser = uow.Repository<User>()
                    .Get(x => x.Email == email)
                    .FirstOrDefault();

                if(existedUser == null)
                {
                    var operatorRole = uow.Repository<Role>()
                        .Get(x => x.RoleName == RoleConstatns.Operator)
                        .FirstOrDefault();

                    if(operatorRole != null)
                    {
                        var passHash = HashHelper.GetPasswordHash(password);

                        var user = new User()
                        {
                            Email = email,
                            PasswordHash = passHash,
                            Role = operatorRole
                        };

                        uow.Repository<User>().Add(user);
                        uow.Save();

                        return new OperatorModel()
                        {
                            Email = user.Email,
                            Id = user.Id
                        };
                    }
                }

                return null;
            });
        }

        public void DeleteOperator(Guid id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var existedOperator = uow.Repository<User>()
                    .Get(id);

                if (existedOperator != null)
                {
                    uow.Repository<User>().Delete(existedOperator);
                    uow.Save();
                }
            });
        }

        public IEnumerable<OperatorModel> GetOperators()
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                return uow.Repository<User>()
                    .Include(x => x.Role)
                    .Where(x => x.Role.RoleName == RoleConstatns.Operator)
                    .Select(x => new OperatorModel()
                    {
                        Email = x.Email,
                        Id = x.Id
                    });
            });
        }
    }
}
