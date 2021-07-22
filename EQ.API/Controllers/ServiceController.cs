using EQ.BLL.Abstractions;
using EQ.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using EQ.Models.Models.DTO;

namespace EQ.API.Controllers
{
    [Authorize(Roles = RoleConstatns.Admin)]

    public class ServiceController : BaseController
    {
        private IServiceService serviceService;

        public ServiceController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.serviceService = serviceProvider.GetService<IServiceService>();
        }

        [HttpPost]
        public IActionResult CreateService([FromBody] ServiceModel model)
        {
            return this.ReturnOkIfExist(this.serviceService.CreateService(model.ServiceName, model.Priority));
        }

        [HttpGet]
        public IActionResult GetServices()
        {
            return this.Ok(this.serviceService.GetServices());
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteService(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                this.serviceService.DeleteService(guid);
                return this.Ok();
            }

            return this.BadRequest();
        }
    }
}
