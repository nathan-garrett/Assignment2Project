using Assignment2Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.ViewModel
{
    public class ReportViewModel
    {
        public ReportModel Report { get; set; }

        public IEnumerable<SelectListItem> AssetList { get; set; }
    }
}
