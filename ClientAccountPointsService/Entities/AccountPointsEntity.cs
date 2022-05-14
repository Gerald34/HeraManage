using System.ComponentModel.DataAnnotations;
namespace ClientAccountPointsService.Entities
{
    public class AccountPointsEntity
    {
        [Key]
        public Guid uid { get; set; }
        public int Points { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}