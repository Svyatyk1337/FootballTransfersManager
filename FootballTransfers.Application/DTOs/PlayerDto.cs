using FootballTransfers.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Application.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public string Nationality { get; set; } = string.Empty;
        public Position Position { get; set; }
        public string PositionName => Position.ToString();
        public int Height { get; set; }
        public int Weight { get; set; }
        public decimal MarketValue { get; set; }
        public ClubDto? CurrentClub { get; set; }
        public AgentDto? Agent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CurrentClubId { get; set; }
        public int? AgentId { get; set; }
    }

    public class CreatePlayerDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        public Position Position { get; set; }

        [Range(150, 220)]
        public int Height { get; set; }

        [Range(50, 120)]
        public int Weight { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MarketValue { get; set; }

        public int? CurrentClubId { get; set; }
        public int? AgentId { get; set; }
    }

    public class UpdatePlayerDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        public Position Position { get; set; }

        [Range(150, 220)]
        public int Height { get; set; }

        [Range(50, 120)]
        public int Weight { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MarketValue { get; set; }

        public int? CurrentClubId { get; set; }
        public int? AgentId { get; set; }
    }
}