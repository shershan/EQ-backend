using System;

namespace EQ.Models.Models.DTO
{
    public class ServiceModel
    {
        public Guid Id
        {
            get;
            set;
        }

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
    }
}
