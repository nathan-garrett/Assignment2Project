using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Models
{
    public class ReportModel
    {
        [Key]
        public int ReportId { get; set; }

        [Display(Name = "Report Type")]
        public ReportType RType { get; set; }

        [Display(Name = "Issue Details")]
        public string IssueDetails { get; set; }

        public int AssetId { get; set; }   
        
        [ForeignKey("AssetId")]
        public AssetModel Asset { get; set; }

        [Display(Name = "Date/Time")]        
        public DateTime ReportDTS { get; set; }

        [Display(Name ="Created By")]
        [EmailAddress]
        public string CreatedByUserEmail { get; set; }

        [Display(Name ="Who Reported Issue")]
        public string WhoReportedIssue { get; set; }

        public TicketStatus Status { get; set; }

        public List <UpdateResolutionModel> UpdateResolve { get; set; }

        public ReportModel()
        {
            UpdateResolve = new List<UpdateResolutionModel>();
        }
    }
}
