using System.Collections.Generic;

namespace EQ.DAL.Models
{
    public class Role : BaseEntity
    {
        public string RoleName
        {
            get;
            set;
        }

        public virtual ICollection<User> Users
        {
            get;
            set;
        }
    }
}
