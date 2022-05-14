using System.ComponentModel.DataAnnotations;

namespace ClientAccountService.Entities
{
    public class AccountTypesEntity
    {
        [Key]
        public int TypeID { get; set; }
        public string TypeName { get; set; } = string.Empty;
    }
}