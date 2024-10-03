using Infrastructure.Models;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Common;
public static class PaginationExtensions
{
    public static async Task<Pagination<TItem>> PaginateAsync<TItem, TId>(
        this IQueryable<TItem> items,
        PaginationMeta meta,
        CancellationToken token) where TItem : ITableEntity<TId>
                                 where TId : notnull
    {
        var result = await items
            .ThenOrBy(x => x.Id)
            .Skip((meta.CurrentPage - 1) * meta.PageSize)
            .Take(meta.PageSize)
            .ToListAsync(token: token);

        var count = await items.CountAsync(token: token);

        return new Pagination<TItem>(result, meta.Recreate(result, count));
    }
}