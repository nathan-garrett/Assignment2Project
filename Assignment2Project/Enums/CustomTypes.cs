using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Models
{ 
        public enum ReportType
        {
            General = 1,
            Technical = 2
        }

        public enum Roles
        {
            IT_Manager,
            IT_Support,
            Staff,
            Student
        }

        public enum TicketStatus
    {
        Open = 1,
        Updated = 2,
        Closed = 3
    }

}
