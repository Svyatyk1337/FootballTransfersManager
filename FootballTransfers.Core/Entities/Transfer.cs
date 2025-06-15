using System.ComponentModel.DataAnnotations;

namespace FootballTransfers.Core.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        
        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;
        
        public int? FromClubId { get; set; }
        public Club? FromClub { get; set; }
        
        public int ToClubId { get; set; }
        public Club ToClub { get; set; } = null!;
        
        public decimal TransferFee { get; set; } // в євро
        
        public DateTime TransferDate { get; set; }
        
        public TransferType TransferType { get; set; }
        
        [MaxLength(20)]
        public string ContractLength { get; set; } = string.Empty; 
        
        public string? Description { get; set; }
        
        public bool IsConfirmed { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}