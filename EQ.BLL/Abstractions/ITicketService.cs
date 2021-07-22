using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;

namespace EQ.BLL.Abstractions
{
    public interface ITicketService
    {
        TicketModel CreateTicket(Guid serviceId);

        IEnumerable<TicketModel> GetTickets();

        IEnumerable<TicketModel> GetTicket(Guid id);

        IEnumerable<TicketModel> GetTicketByService(Guid serviceId);

        TicketModel InProgress(Guid ticketId, Guid windowId);

        TicketModel Complete(Guid ticketId);

        TicketModel Cancel(Guid ticketId);

        TicketModel FindNextTicket(Guid windowId);
    }
}
