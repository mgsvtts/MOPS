using Infrastructure.Misc.Queries.MerchItems;
using Infrastructure.Models;

namespace Infrastructure;

public partial class Queries
{
    public static class MerchItem
    {
        public static string AddMerchItem()
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

        public static string GetAllMerchItems(bool showNotAvailable, MerchItemSort sort)
        {
            var query = @$"SELECT m.*, i.{nameof(images.id)}, i.{nameof(images.url)}, i.{nameof(images.is_main)}
                          FROM {nameof(merch_items)} AS m
                          LEFT JOIN {nameof(images)} i ON i.{nameof(images.merch_item_id)} = m.{nameof(merch_items.id)}
                          WHERE i.{nameof(images.is_main)} = 1 ";
            
            if (!showNotAvailable)
            {
                query += $"AND {nameof(merch_items.amount)} > 0 ";
            }

            query += AddSorting(sort);

            return query;
        }

        public static string GetAllByIds()
        {
            return $"SELECT * FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} IN @Ids";
        }

        public static string GetById()
        {
            return $"SELECT * FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)} LIMIT(1)";
        }

        public static string Delete()
        {
            return $"DELETE FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)}";
        }

        public static string Update()
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

        private static string AddSorting(MerchItemSort sort){
            return sort switch
            {
                MerchItemSort.NameAsc => $"ORDER BY {nameof(merch_items.name)}",
                MerchItemSort.NameDesc => $"ORDER BY {nameof(merch_items.name)} DESC",
                MerchItemSort.AmountAsc => $"ORDER BY {nameof(merch_items.amount)}",
                MerchItemSort.AmountDesc => $"ORDER BY {nameof(merch_items.amount)} DESC",
                MerchItemSort.CreatedAsc => $"ORDER BY {nameof(merch_items.created_at)}",
                MerchItemSort.CreatedDesc => $"ORDER BY {nameof(merch_items.created_at)} DESC",
                MerchItemSort.DescriptionAsc => $"ORDER BY {nameof(merch_items.description)}",
                MerchItemSort.DescriptionDesc => $"ORDER BY {nameof(merch_items.description)} DESC",
                MerchItemSort.PriceAsc => $"ORDER BY {nameof(merch_items.price)}",
                MerchItemSort.PriceDesc => $"ORDER BY {nameof(merch_items.price)} DESC",
                MerchItemSort.SelfPriceAsc => $"ORDER BY {nameof(merch_items.self_price)}",
                MerchItemSort.SelfPriceDesc => $"ORDER BY {nameof(merch_items.self_price)} DESC",
                _ => throw new NotImplementedException(),
            };

        }
    }
}
