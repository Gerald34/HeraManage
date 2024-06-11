using System.ComponentModel.DataAnnotations;
using ClientAccountService.Entities;

namespace ClientAccountService.Requests
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
        public Int64 RsaIdNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public AccountTypes AccountType { get; set; }
    }
}