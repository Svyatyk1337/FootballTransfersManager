namespace FootballTransfers.Application.Pagination
{
    public class ClubFilterParams
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? League { get; set; }
        public string? SortBy { get; set; } // "name", "founded"
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
