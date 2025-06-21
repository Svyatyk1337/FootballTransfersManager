using FootballTransfers.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Application.DTOs
{
    public class TransferDto
    {
        public int Id { get; set; }
        public PlayerDto Player { get; set; } = null!;
        public ClubDto? FromClub { get; set; }
        public ClubDto ToClub { get; set; } = null!;
        public decimal TransferFee { get; set; }
        public DateTime TransferDate { get; set; }
        public TransferType TransferType { get; set; }
        public string TransferTypeName => TransferType.ToString();
        public string ContractLength { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateTransferDto
    {
        [Required]
        public int PlayerId { get; set; }

        public int? FromClubId { get; set; }

        [Required]
        public int ToClubId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TransferFee { get; set; }

        [Required]
        public DateTime TransferDate { get; set; }

        [Required]
        public TransferType TransferType { get; set; }

        [MaxLength(20)]
        public string ContractLength { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsConfirmed { get; set; } = false;
    }

    public class UpdateTransferDto
    {
        [Required]
        public int PlayerId { get; set; }

        public int? FromClubId { get; set; }

        [Required]
        public int ToClubId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TransferFee { get; set; }

        [Required]
        public DateTime TransferDate { get; set; }

        [Required]
        public TransferType TransferType { get; set; }

        [MaxLength(20)]
        public string ContractLength { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsConfirmed { get; set; }
    }
}