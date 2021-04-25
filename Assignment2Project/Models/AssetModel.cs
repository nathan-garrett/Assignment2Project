using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Models
{
    public class AssetModel
    {
        [Key]
        public int AssetId { get; set; }

        [Required]
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }

    }
}
