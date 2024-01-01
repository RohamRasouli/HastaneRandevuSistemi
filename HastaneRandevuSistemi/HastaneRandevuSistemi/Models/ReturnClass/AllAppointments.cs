namespace HastaneRandevuSistemi.Models.ReturnClass
{
    public class AllAppointments
    {
        public int? AppointmentId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserSecondName { get; set; }
        public string? doctorFirstName { get; set; }
        public string? doctorSecondName { get; set; }
        public string? PoliclinicName { get; set; }
        public string? ScienceName { get; set; }
        //public int? StatuId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? CreatetDate { get; set; }
       
    }
}
