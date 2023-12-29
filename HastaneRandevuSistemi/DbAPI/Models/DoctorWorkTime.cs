using System;
using System.Collections.Generic;

namespace DbAPI.Models
{
    public partial class DoctorWorkTime
    {
        public int DoctorWorkTimesId { get; set; }
        public int? DoctorId { get; set; }
        public int? PoliclinicId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? MainBranchId { get; set; }
    }
}
