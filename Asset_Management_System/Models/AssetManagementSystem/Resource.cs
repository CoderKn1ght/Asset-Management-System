using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ResourceName { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Facility")]
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}