namespace LocalNativeProvider.Controllers.Dtos;

public record PaginatedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public string NextPageUrl { get; set; }
}