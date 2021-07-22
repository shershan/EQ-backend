using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EQ.DAL.Models
{
    public class User: IdentityUser<Guid>, IBaseEntity
    {
        public Guid RoleId
        {
            get;
            set;
        }

        public virtual Role Role
        {
            get;
            set;
        }

        public virtual ICollection<Window> Windows
        {
            get;
            set;
        }
    }
}
