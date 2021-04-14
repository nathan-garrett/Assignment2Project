using ReportManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Models
{
    public class ReportModel
    {
        [Key]
        public int Id { get; set; }

        public CustomTypes ReportType { get; set; }

        public string IssueDetails { get; set; }

        public string UserOpenedTicket { get; set; }

        public List<AssetsModel> Asset { get; set; }

        public DateTime ReportDTS { get; set; }




    }
}
