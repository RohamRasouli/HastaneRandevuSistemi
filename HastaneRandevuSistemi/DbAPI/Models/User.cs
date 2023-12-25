using System;
using System.Collections.Generic;

namespace DbAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserSecondName { get; set; }
        public string? UserEmail { get; set; }
        public int? UserTypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? Password { get; set; }
    }
}
