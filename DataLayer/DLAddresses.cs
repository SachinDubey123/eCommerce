using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer.DataLayer
{
    public class DLAddresses
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public int PinCode { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        
       
        public string AddressType { get; set; }

        public DLAddresses()
        {
            this.Countries = new List<SelectListItem>();
            this.States = new List<SelectListItem>();
            this.Cities = new List<SelectListItem>();
        }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }
        

    }
}
