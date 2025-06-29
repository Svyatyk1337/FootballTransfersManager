using FootballTransfers.Core.Entities;

namespace FootballTransfers.Application.Pagination
{
    public class PlayerFilterParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public Position? Position { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public decimal? MinMarketValue { get; set; }
        public decimal? MaxMarketValue { get; set; }

        public string? SortBy { get; set; } = "LastName"; 
        public bool IsDescending { get; set; } = false;
    }
}
