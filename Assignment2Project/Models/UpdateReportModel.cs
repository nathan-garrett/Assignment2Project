using Assignment2Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Models
{
    public class UpdateReportModel : ReportViewModel
    {
        public DateTime DateTimeOfAction { get; set; }

        public int MyProperty { get; set; }

        public string UserUpdating { get; set; }

        public string Notes { get; set; }


    }
}
