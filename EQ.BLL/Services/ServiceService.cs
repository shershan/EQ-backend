using EQ.BLL.Abstractions;
using EQ.DAL.Models;
using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EQ.BLL.Services
{
    public class ServiceService : BaseDbService.BaseDbServices, IServiceService
    {
        public ServiceService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public ServiceModel CreateService(string name, int priority = 0)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var existedService = uow.Repository<Service>()
                    .Get(x => x.ServiceName == name)
                    .FirstOrDefault();

                if (existedService == null)
                {
                    var service = new Service()
                    {
                        ServiceName = name,
                        Priority = priority
                    };

                    uow.Repository<Service>().Add(service);
                    uow.Save();

                    return new ServiceModel()
                    {
                        Id = service.Id,
                        Priority = service.Priority,
                        ServiceName = service.ServiceName
                    };
                }

                return null;
            });
        }

        public void DeleteService(Guid id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var service = uow.Repository<Service>()
                    .Get(id);

                if(service != null)
                {
                    uow.Repository<Service>().Delete(service);
                    uow.Save();
                }
            });
        }

        public IEnumerable<ServiceModel> GetServices()
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                return uow.Repository<Service>().Get().Select(x => new ServiceModel()
                {
                    Priority = x.Priority,
                    ServiceName = x.ServiceName,
                    Id = x.Id,
                });
            });
        }
    }
}
