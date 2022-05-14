using System.ComponentModel.DataAnnotations;

namespace ClientAccountService.Requests
{
    public class RegistrationRequest
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dob { get; set; }

        public int gender { get; set; }
    }
}

