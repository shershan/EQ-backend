using System;

namespace EQ.DAL.Models
{
    public interface IBaseEntity
    {
        Guid Id
        {
            get;
            set;
        }
    }
}
