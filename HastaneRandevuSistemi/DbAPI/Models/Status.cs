using System;
using System.Collections.Generic;

namespace DbAPI.Models
{
    public partial class Status
    {
        public int StatuId { get; set; }
        public string? StatuName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
