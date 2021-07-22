using EQ.Models.Enums;
using System;

namespace EQ.Models.Models.DTO
{
    public class TicketModel
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid ServiceId
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public Guid? WindowId
        {
            get;
            set;
        }

        public string WindowName
        {
            get;
            set;
        }

        public DateTime CreatedTime
        {
            get;
            set;
        }

        public DateTime? CompletedDateTime
        {
            get;
            set;
        }

        public ServiceStatus ServiceStatus
        {
            get;
            set;
        }
    }
}
