using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Application.DTOs
{
    public class AgentDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public string? Company { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int PlayersCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateAgentDto
    {
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
    }

    public class UpdateAgentDto
    {
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
    }
}
