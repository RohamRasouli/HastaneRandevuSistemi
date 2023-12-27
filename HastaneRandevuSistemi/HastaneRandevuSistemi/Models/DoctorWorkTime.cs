namespace HastaneRandevuSistemi.Models
{
    public class DoctorWorkTime
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
