using System;

namespace EQ.Models.Models.DTO
{
    public class AssignedWindowModel
    {
        public Guid WindowId
        {
            get;
            set;
        }

        public string WindowName
        {
            get;
            set;
        }

        public Guid? ServiceId
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public Guid? OperatorId
        {
            get;
            set;
        }

        public string OperaroeName
        {
            get;
            set;
        }

        public bool IsOpen
        {
            get;
            set;
        }
    }
}
