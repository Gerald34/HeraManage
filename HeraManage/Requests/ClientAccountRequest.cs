using System.ComponentModel.DataAnnotations;
using HeraManage.Entities;

namespace HeraManage.Requests
{
    public class ClientAccountRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Int64 RSAIdNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public AccountTypes AccountType { get; set; }
    }
}