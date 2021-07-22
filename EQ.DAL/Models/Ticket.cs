using EQ.Models.Enums;
using System;

namespace EQ.DAL.Models
{
    public class Ticket : BaseEntity
    {
        public DateTime CreatedTime
        {
            get;
            set;
        } 

        public DateTime? FinishedTime
        {
            get;
            set;
        }

        public ServiceStatus ServiceStatus
        {
            get;
            set;
        }

        public Guid? WindowId
        {
            get;
            set;
        }

        public Guid ServiceId
        {
            get;
            set;
        }

        public virtual Window Window
        {
            get;
            set;
        }

        public virtual Service Service
        {
            get;
            set;
        }
    }
}
