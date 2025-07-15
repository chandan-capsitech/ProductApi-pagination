namespace ProductApi.Models
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalItems { get; set; }
    }
}