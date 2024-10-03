using Mapster;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Application.Queries.Common;

public readonly record struct Pagination<T>
{
    public IEnumerable<T> Items { get; }

    [JsonPropertyName("_links")]
    public PaginationLinks Links { get; }

    [JsonPropertyName("_meta")]
    public PaginationMeta Meta { get; }

    public Pagination(IEnumerable<T> items, PaginationMeta meta)
    {
        Meta = meta;
        Items = items;
        Links = new PaginationLinks(meta);
    }

    public Pagination<U> MapItemsTo<U>()
    {
        return new Pagination<U>(Items.Adapt<IEnumerable<U>>(), Meta);
    }
}

public readonly record struct PaginationLinks
{
    private static string _baseUrl = "";

    public Uri? Self { get; }
    public Uri? Next { get; }
    public Uri First { get; }
    public Uri? Last { get; }

    public PaginationLinks(PaginationMeta meta)
    {
        Self = meta.CurrentPage > 0 ? new Uri($"{_baseUrl}{meta.Api.SetPage(meta.CurrentPage)}") : null;
        Next = meta.CurrentPage < meta.LastPage ? new Uri($"{_baseUrl}{meta.Api.SetPage(meta.CurrentPage + 1)}") : null;
        First = new Uri($"{_baseUrl}{meta.Api.SetPage(1)}");
        Last = meta.LastPage > 0 ? new Uri($"{_baseUrl}{meta.Api.SetPage(meta.LastPage)}") : null;
    }

    public static void SetUrls(string baseUrl)
    {
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new ArgumentNullException(nameof(baseUrl), "base url for pagination cannot be null");
        }
        _baseUrl = baseUrl; 
    }
}

public readonly record struct PaginationMeta
{
    public long Total { get; }
    public int LastPage { get; }   
    public int CurrentPage { get; }
    public int PageSize => 20;

    [JsonIgnore]
    public PaginationApi Api { get; }

    public PaginationMeta(int currentPage, PaginationApi api)
    {
        if (currentPage <= 0)
        {
            throw new InvalidOperationException("Current page cannot be less than 1");
        }

        Api = api;
        CurrentPage = currentPage;
    }

    public PaginationMeta(int currentPage,
                          PaginationApi api, 
                          int lastPage,
                          int total) : this(currentPage, api)
    {
        if(currentPage <= 0)
        {
            throw new InvalidOperationException("Current page cannot be less than 1");
        }

        if(currentPage > lastPage)
        {
            throw new InvalidOperationException("Current page cannot be greater than last page");
        }

        if (total < 0)
        {
            throw new InvalidOperationException("Total items cannot be less than 0");
        }

        if(lastPage < 0)
        {
            throw new InvalidOperationException("Last page cannot be less than 0");
        }

        Total = total;
        LastPage = lastPage;
        CurrentPage = currentPage;
    }

    public PaginationMeta Recreate<T>(ICollection<T> items, int total)
    {
        var pageSize = items.Count < PageSize ? items.Count : PageSize;
        var lastPage = total == pageSize ? CurrentPage : total / PageSize + 1;

        return new PaginationMeta(CurrentPage,
                                  Api,
                                  lastPage,
                                  total);
    }
}

public readonly partial record struct PaginationApi
{
    public string Path { get; }
    public string? Query { get; }

    public PaginationApi(string path, string query)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path), "Path of the pagination api cannot be null");
        }

        Path = path;
        Query = query;
    }

    public PaginationApi SetPage(int page)
    {
        if(Query is null)
        {
            return this;
        }

        var replaced = UriRegex().Replace(Query, "");

        replaced = replaced.Replace("&&", "&").Trim('&', ',', '?');

        var result = !string.IsNullOrEmpty(replaced) ? $"?page={page}&{replaced}" : $"?page={page}";

        return new PaginationApi(Path, result);
    }

    public override string ToString()
    {
        return $"{Path}{Query}";
    }

    [GeneratedRegex(@"page=\d+&?", RegexOptions.Compiled, 5000)]
    private static partial Regex UriRegex();
}