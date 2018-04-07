using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class FacilityViewModel
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public bool IsActive { get; set; }
        public string FacilityDescription { get; set; }
        public string FacilityAddress { get; set; }
        public List<FacilityResourceViewModel> Resources { get; set; }
    }
}