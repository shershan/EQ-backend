using System;

namespace EQ.DAL.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id
        {
            get;
            set;
        }
    }
}
