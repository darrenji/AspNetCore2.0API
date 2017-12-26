using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Models
{
    public class MovieInputModel
    {
        [Required]
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
    }
}
