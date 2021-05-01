using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Models
{
    public class UpdateResolutionModel
    {
        [Key]
        public int UpdateResolveId { get; set; }

        public string StaffMemberActioning { get; set; }

        public string Text { get; set; }

        public DateTime UpdateResolveDTS { get; set; }

        public int IssueId { get; set; }

    }
}
