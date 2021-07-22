using System;
using System.Collections.Generic;

namespace EQ.DAL.Models
{
    public class Window : BaseEntity
    {
        public string  WindowName
        {
            get;
            set;
        }

        public bool IsOpen
        {
            get;
            set;
        }

        public Guid UserId
        {
            get;
            set;
        }

        public Guid ServiceId
        {
            get;
            set;
        }

        public virtual User User
        {
            get;
            set;
        }

        public virtual Service Service
        {
            get;
            set;
        }

        public virtual ICollection<Request> Requests
        {
            get;
            set;
        }
    }
}
