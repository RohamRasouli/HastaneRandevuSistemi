using System;
using System.Collections.Generic;

namespace DbAPI.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int? DoctorId { get; set; }
        public int? MainScienceBranchId { get; set; }
        public int? UserId { get; set; }
        public int? StatuId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? CreatetDate { get; set; }
        public int? PoliclinicId { get; set; }
    }
}
