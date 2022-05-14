using System.ComponentModel.DataAnnotations;
namespace HeraManage.Entities
{
    public class ClientEntity
    {
        [Key]
        public Guid uid { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Int64 RSAIdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int active { get; set; }
        public bool verified { get; set; }

    }
}