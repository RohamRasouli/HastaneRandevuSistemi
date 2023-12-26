using System;
using System.Collections.Generic;

namespace DbAPI.Models
{
    public partial class Policlinic
    {
        public int PoliclinicId { get; set; }
        public string? PoliclinicName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
