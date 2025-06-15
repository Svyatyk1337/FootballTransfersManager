using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Core.Entities
{
    public class Player
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(50)]
        public string Nationality { get; set; } = string.Empty;
        
        public Position Position { get; set; }
        
        public int Height { get; set; } 
        
        public int Weight { get; set; } 
        
        public decimal MarketValue { get; set; } 
        
        public int? CurrentClubId { get; set; }
        public Club? CurrentClub { get; set; }
        
        public int? AgentId { get; set; }
        public Agent? Agent { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();

    } 
}