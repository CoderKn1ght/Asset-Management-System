using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            Facilities = new List<FacilityViewModel>();
        }
        public List<FacilityViewModel> Facilities { get; set; }
    }
}