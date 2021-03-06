﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress]
        public string EmailId { get; set; }
        public bool IsActive { get; set; } = true;
        
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}