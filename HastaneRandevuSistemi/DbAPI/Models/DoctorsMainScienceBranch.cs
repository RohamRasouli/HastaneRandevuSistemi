using System;
using System.Collections.Generic;

namespace DbAPI.Models
{
    public partial class DoctorsMainScienceBranch
    {
        public int DoctorScienceId { get; set; }
        public int? DoctorId { get; set; }
        public int? MainScienceBranchId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
