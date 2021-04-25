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

        [Required]
        public int AssetId { get; set; }   
        
        [ForeignKey("AssetId")]
        public AssetModel Asset { get; set; }

        [Display(Name = "Date/Time")]        
        public DateTime ReportDTS { get; set; }

        

    }
}
