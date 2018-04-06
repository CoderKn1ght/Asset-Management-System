using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class Facility
    {
        public Facility()
        {
            Resources = new HashSet<Resource>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(60)]
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}