using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Core.Entities
{
    public class Club
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string League { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;
        
        public DateTime Founded { get; set; }
        
        [MaxLength(100)]
        public string Stadium { get; set; } = string.Empty;
        
        public string? LogoUrl { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public ICollection<Player> CurrentPlayers { get; set; } = new List<Player>();
        public ICollection<Transfer> TransfersFrom { get; set; } = new List<Transfer>();
        public ICollection<Transfer> TransfersTo { get; set; } = new List<Transfer>();
    }
}