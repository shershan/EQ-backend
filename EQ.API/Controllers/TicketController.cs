using EQ.BLL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using EQ.Models.Models.DTO;
using EQ.Constants;

namespace EQ.API.Controllers
{
    [Authorize(Roles = RoleConstatns.Operator)]
    public class TicketController : BaseController
    {
        private readonly ITicketService ticketService;

        public TicketController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.ticketService = serviceProvider.GetService<ITicketService>();
        }

        //TicketModel CreateTicket(Guid serviceId);
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateTicket([FromBody] TicketModel model)
        {
            return this.ReturnOkIfExist(this.ticketService.CreateTicket(model.ServiceId));
        }

        //IEnumerable<TicketModel> GetTickets();
        [HttpGet]
        public IActionResult GetAllTickets()
        {
            return this.ReturnOkIfExist(this.ticketService.GetTickets());
        }

        //IEnumerable<TicketModel> GetTicket(Guid id);
        [HttpGet("{id}")]
        public IActionResult GetAllTicket(string id)
        {
            if (Guid.TryParse(id, out var ticketId))
            {
                return this.ReturnOkIfExist(this.ticketService.GetTicket(ticketId));
            }

            return this.BadRequest();
        }

        //IEnumerable<TicketModel> GetTicketByService(Guid serviceId);
        [HttpGet("service/{id}")]
        public IActionResult GetTicketsByServiceId(string id)
        {
            if (Guid.TryParse(id, out var serviceId))
            {
                return this.ReturnOkIfExist(this.ticketService.GetTicketByService(serviceId));
            }

            return this.BadRequest();
        }

        //TicketModel InProgress(Guid ticketId, Guid windowId);
        [HttpPost("assigneToWindow")]
        public IActionResult AssigneToWindow([FromBody] TicketModel model)
        {
            return this.ReturnOkIfExist(this.ticketService.InProgress(model.Id, model.WindowId.Value));
        }

        //TicketModel Complete(Guid ticketId);
        [HttpPost("complete/{id}")]
        public IActionResult CompleteTicket(string id)
        {
            if (Guid.TryParse(id, out var ticketId))
            {
                return this.ReturnOkIfExist(this.ticketService.Complete(ticketId));
            }

            return this.BadRequest();
        }


        //TicketModel Cancel(Guid ticketId);
        [HttpPost("cancel/{id}")]
        public IActionResult CancelTicket(string id)
        {
            if (Guid.TryParse(id, out var ticketId))
            {
                return this.ReturnOkIfExist(this.ticketService.Cancel(ticketId));
            }

            return this.BadRequest();
        }


        //TicketModel FindNextTicket(Guid windowId);
        [HttpGet("next/{id}")]
        public IActionResult FindNextTicket(string id)
        {
            if (Guid.TryParse(id, out var windowId))
            {
                return this.ReturnOkIfExist(this.ticketService.FindNextTicket(windowId));
            }

            return this.BadRequest();
        }
    }
}
