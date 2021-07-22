using EQ.API.Models;
using EQ.BLL.Abstractions;
using EQ.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace EQ.API.Controllers
{
    [Authorize(Roles = RoleConstatns.Admin)]
    public class OperatorController : BaseController
    {
        private IOperatorService operatorService;

        public OperatorController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.operatorService = this.serviceProvider.GetService<IOperatorService>();
        }

        [HttpPost]
        public IActionResult CreateOperator([FromBody] LoginModel model)
        {
            return this.ReturnOkIfExist(this.operatorService.CreateOperator(model.Email, model.Password));
        }

        [HttpGet]
        public IActionResult GetOperators()
        {
            return this.Ok(this.operatorService.GetOperators());
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteOperator(string id)
        {
            if(Guid.TryParse(id, out var guid))
            {
                this.operatorService.DeleteOperator(guid);
                return this.Ok();
            }

            return this.BadRequest();
        }
    }
}
