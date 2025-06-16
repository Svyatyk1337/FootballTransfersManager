using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Application.DTOs
{
    public class ClubDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string League { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime Founded { get; set; }
        public string Stadium { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public int PlayersCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateClubDto
    {
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
    }

    public class UpdateClubDto
    {
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
    }
}