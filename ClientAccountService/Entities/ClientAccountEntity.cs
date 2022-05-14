using System.ComponentModel.DataAnnotations;

namespace ClientAccountService.Entities
{
    public enum AccountTypes
    {
        SAVINGS, CHEQUE, INVEST
    }
    public class ClientAccountEntity
    {
        [Key]
        public Guid uid { get; set; }
        public int AccountNumber { get; set; }
        public AccountTypes AccountType { get; set; }
        public int Active { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}