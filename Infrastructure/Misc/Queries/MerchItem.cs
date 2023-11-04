using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Misc.Queries;

internal partial class Queries
{
    internal static class MerchItem
    {
        internal static string AddMerchItem()
        {
            return @$"INSERT INTO {nameof(merch_items)} ({nameof(merch_items.id)},
                                                          {nameof(merch_items.type_id)},
                                                          {nameof(merch_items.name)},
                                                          {nameof(merch_items.description)},
                                                          {nameof(merch_items.price)},
                                                          {nameof(merch_items.self_price)},
                                                          {nameof(merch_items.amount)},
                                                          {nameof(merch_items.created_at)})
                       VALUES (@{nameof(merch_items.id)},
                               @{nameof(merch_items.type_id)},
                               @{nameof(merch_items.name)},
                               @{nameof(merch_items.description)},
                               @{nameof(merch_items.price)},
                               @{nameof(merch_items.self_price)},
                               @{nameof(merch_items.amount)},
                               @{nameof(merch_items.created_at)})";
        }

        internal static string GetAllMerchItems(bool showNotAvailable)
        {
            return showNotAvailable
               ? $"SELECT * FROM {nameof(merch_items)}"
               : $"SELECT * FROM {nameof(merch_items)} WHERE {nameof(merch_items.amount)} > 0";
        }

        internal static string GetAllByIds()
        {
            return $"SELECT * FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} IN @Ids";
        }

        internal static string GetById()
        {
            return $"SELECT * FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)} LIMIT(1)";
        }

        internal static string Delete()
        {
            return $"DELETE FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)}";
        }

        internal static string Update()
        {
            return @$"UPDATE {nameof(merch_items)} SET
                    {nameof(merch_items.type_id)} = @{nameof(merch_items.type_id)},
                    {nameof(merch_items.name)} = @{nameof(merch_items.name)},
                    {nameof(merch_items.description)} = @{nameof(merch_items.description)},
                    {nameof(merch_items.price)} = @{nameof(merch_items.price)},
                    {nameof(merch_items.self_price)} = @{nameof(merch_items.self_price)},
                    {nameof(merch_items.amount)} = @{nameof(merch_items.amount)}
                     WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)}";
        }
    }
}