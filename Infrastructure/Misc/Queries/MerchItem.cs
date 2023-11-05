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

        public static string GetAllMerchItems(bool showNotAvailable)
        {
            if (showNotAvailable)
            {
                return @$"SELECT m.*, i.{nameof(images.id)}, i.{nameof(images.url)}, i.{nameof(images.is_main)}
                          FROM {nameof(merch_items)} AS m
                          LEFT JOIN {nameof(images)} i ON i.{nameof(images.merch_item_id)} = m.{nameof(merch_items.id)}
                          WHERE i.{nameof(images.is_main)} = 1";
            }
            else
            {
                return @$"SELECT m.*, i.{nameof(images.id)}, i.{nameof(images.url)}, i.{nameof(images.is_main)}
                          FROM {nameof(merch_items)} AS m
                          LEFT JOIN {nameof(images)} i ON i.{nameof(images.merch_item_id)} = m.{nameof(merch_items.id)}
                          WHERE i.{nameof(images.is_main)} = 1 AND {nameof(merch_items.amount)} > 0";
            }
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
    }
}
