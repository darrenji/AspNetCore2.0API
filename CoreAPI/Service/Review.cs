using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Service
{
    public class Review
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }
    }
}
