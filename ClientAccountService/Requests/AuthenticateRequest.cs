

using System.ComponentModel.DataAnnotations;
namespace HeraManage.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string username { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;
    }
}
