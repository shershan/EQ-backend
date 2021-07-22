using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;

namespace EQ.BLL.Abstractions
{
    public interface IServiceService
    {
        ServiceModel CreateService(string name, int priority = 0);

        IEnumerable<ServiceModel> GetServices();

        void DeleteService(Guid id);
    }
}
