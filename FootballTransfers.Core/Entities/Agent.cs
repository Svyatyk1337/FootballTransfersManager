using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Core.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Company { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }
        
        [Phone]
        public string? Phone { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
} 