using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class FacilityResourceViewModel
    {
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public int Quantity { get; set; }
        public string resourceCheckerComments { get; set; }
        public string AdminComments { get; set; }
        public bool isChanged { get; set; }
    }
}