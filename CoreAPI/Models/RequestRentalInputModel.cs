using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Models
{
    public class RequestRentalInputModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public int Days { get; set; }
    }
}
