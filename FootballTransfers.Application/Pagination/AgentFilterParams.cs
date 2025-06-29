namespace FootballTransfers.Application.Pagination
{
    public class AgentFilterParams
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? SortBy { get; set; } // "name", "playersCount"
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
