using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domains;

namespace Web.ViewModels
{
    public class StandardIndexViewModel
    {
        public IEnumerable<FacilityViewModel> Facilities { get; set; }


        public StandardIndexViewModel(IEnumerable<Facility> facility)
        {
            Facilities = facility.Select(x => new FacilityViewModel(x));
        }
    }


}