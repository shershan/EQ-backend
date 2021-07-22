using System.Collections.Generic;

namespace EQ.DAL.Models
{
    public class Service: BaseEntity
    {
        public string ServiceName
        {
            get;
            set;
        }

        public int Priority
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
