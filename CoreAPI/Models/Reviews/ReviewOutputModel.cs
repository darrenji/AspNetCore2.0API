﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Models.Reviews
{
    public class ReviewOutputModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }
        public DateTime LastReadAt { get; set; }
    }
}
