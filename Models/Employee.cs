using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class Employee : DbObject
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String title { get; set; }
        public String titleOfCourtesy { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String region { get; set; }
        public String postalCode { get; set; }
        public String country { get; set; }
        public String phone { get; set; }
        public String extension { get; set; }
        public String notes { get; set; }
    }
}
