using FootballTransfers.Core.Entities;

namespace FootballTransfers.Application.Pagination
{
    public class TransferFilterParams
    {
        public int? PlayerId { get; set; }
        public int? FromClubId { get; set; }
        public int? ToClubId { get; set; }
        public decimal? MinFee { get; set; }
        public decimal? MaxFee { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TransferType? TransferType { get; set; }
        public string? SortBy { get; set; } 
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
