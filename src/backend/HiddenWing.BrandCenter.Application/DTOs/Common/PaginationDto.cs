namespace HiddenWing.BrandCenter.Application.DTOs.Common;

public sealed class PaginationDto
{
    public int Page { get; init; }

    public int PageSize { get; init; }

    public int Total { get; init; }
}

public sealed class PagedResult<T>
{
    public required IReadOnlyList<T> Items { get; init; }

    public required PaginationDto Pagination { get; init; }
}
