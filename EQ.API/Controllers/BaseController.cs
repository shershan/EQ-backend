using Microsoft.AspNetCore.Mvc;
using System;

namespace EQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IServiceProvider serviceProvider;

        public BaseController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IActionResult ReturnOkIfExist<T>(T result) where T : class
        {
            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}
