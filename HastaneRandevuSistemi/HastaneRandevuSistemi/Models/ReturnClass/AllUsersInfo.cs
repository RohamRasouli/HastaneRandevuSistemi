namespace HastaneRandevuSistemi.Models.ReturnClass
{
    public class AllUsersInfo
    {
        public int? UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserSecondName { get; set; }
        public string? UserEmail { get; set; }
        public string? TypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? Password { get; set; }
    }
}
