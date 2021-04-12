using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportManagementSystem.Models
{
    public class AssetsModel
    {
        [Key]
        [Required]
        public int AssetId { get; set; }

        [Required]
        public string AssetName { get; set; }

    }
}
