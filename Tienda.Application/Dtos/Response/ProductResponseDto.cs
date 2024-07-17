namespace Tienda.Application.Dtos.Response
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateUser { get; set; }
        public int? CategoryId { get; set; }
    }
}
