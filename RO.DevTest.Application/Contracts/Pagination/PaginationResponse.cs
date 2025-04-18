namespace RO.DevTest.Application.Contracts.Pagination;

public class PaginationResponse<T>(IEnumerable<T> data, int count, int page, int pageSize)
{
    public int CurrentPage { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
    public int TotalCount { get; set; } = count;
    public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
    public IEnumerable<T> Data { get; set; } = data;
}
