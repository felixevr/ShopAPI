namespace Tienda.Infrastructure.Commons.Bases.Request
{
    public class BaseFiltersRequest : BasePaginationRequest
    {
        public int? NumFilter { get; set; } = null;
        public string? TextFilter { get; set; } = null;
        public int? StateFilter { get; set; } = null;
        public string? StartDate { get; set; } = null;
        public string? EndDate { get; set; } = null;
        public bool? Download { get; set; } = null;
        public int WithProducts { get; set; } = 0;
        public int WithUsers { get; set; } = 0;
        public int WithInvoices { get; set; } = 0;
    }
}
