using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class UserFacilityViewModel
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public bool Assigned { get; set; }   
    }
}