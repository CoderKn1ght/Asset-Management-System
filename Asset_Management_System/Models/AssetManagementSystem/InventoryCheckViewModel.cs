using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class InventoryCheckViewModel
    {
        public int ResourceId { get; set; }
        [Display(Name = "Resource Name")]
        public string ResourceName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Display(Name = "Comments")]
        public string ResourceCheckerComments { get; set; }
        [Display(Name = "Admin Comments")]
        public string AdminComments { get; set; }
        public bool IsValid { get; set; }
        public int FacilityId { get; set; }
    }
}