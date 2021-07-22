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
using EQ.BLL.Services;

namespace EQ.API.Controllers
{
    [Authorize(Roles = RoleConstatns.Admin)]
    public class WindowsController : BaseController
    {
        private IWindowsService windowsService;

        public WindowsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.windowsService = serviceProvider.GetService<IWindowsService>();
        }

        [HttpPost]
        public IActionResult CreateWindow([FromBody] WindowModel model)
        {
            return this.ReturnOkIfExist(this.windowsService.CreateWindow(model.Name));
        }

        [HttpGet]
        public IActionResult GetWindows()
        {
            return this.Ok(this.windowsService.GetWindows());
        }

        [HttpDelete("/id")]
        public IActionResult DeleteWindow(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                this.windowsService.DeleteWindow(guid);
                return this.Ok();
            }

            return this.BadRequest();
        }

        [HttpPost("assigneWindow")]
        public IActionResult AssigneWindow([FromBody] AssignedWindowModel model)
        {
            return this.ReturnOkIfExist(this.windowsService.AssigneWindow(model.WindowId, model.OperatorId, model.WindowId));
        }

        [Authorize(Roles = RoleConstatns.Operator)]
        [HttpPost("changeOpenStatus")]
        public IActionResult ChangeOpenStatus([FromBody] AssignedWindowModel model)
        {
            return this.ReturnOkIfExist(this.windowsService.ChangeOpenStatus(model.WindowId, model.IsOpen));
        }
    }
}
