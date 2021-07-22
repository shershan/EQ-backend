using EQ.Models.Enums;
using System;

namespace EQ.DAL.Models
{
    public class Request : BaseEntity
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

        public virtual Window Window
        {
            get;
            set;
        }
    }
}
