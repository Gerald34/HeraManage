namespace HeraManage.Requests;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string username { get; set; } = string.Empty;

    [Required]
    public string password { get; set; } = string.Empty;
}