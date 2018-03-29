using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset_Management_System.Models.AssetManagementSystem
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Facilities = new List<CheckBoxViewModel>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<CheckBoxViewModel> Facilities { get; set; }
    }
}