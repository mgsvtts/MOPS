using Infrastructure.Models;

namespace Infrastructure;

public partial class Queries
{
    public static class Order
    {
        public static string AddOrder()
        {
            return $@"INSERT INTO {nameof(orders)} ({nameof(orders.id)},
                                                 {nameof(orders.created_at)},
                                                 {nameof(orders.payment_method)})
                             VALUES (@{nameof(orders.id)},
                                     @{nameof(orders.created_at)},
                                     @{nameof(orders.payment_method)})";
        }

        public static string AddOrderItems()
        {
            return $@"INSERT INTO {nameof(order_items)} ({nameof(order_items.id)},
                                                         {nameof(order_items.order_id)},
                                                         {nameof(order_items.merch_item_id)},
                                                         {nameof(order_items.amount)},
                                                         {nameof(order_items.price)},
                                                         {nameof(order_items.self_price)})
                                VALUES (@{nameof(order_items.id)},
                                        @{nameof(order_items.order_id)},
                                        @{nameof(order_items.merch_item_id)},
                                        @{nameof(order_items.amount)},
                                        @{nameof(order_items.price)},
                                        @{nameof(order_items.self_price)})";
        }

        public static string GetById()
        {
            return $"SELECT * FROM {nameof(orders)} WHERE id = @{nameof(orders.id)} LIMIT (1)";
        }

        public static string Delete()
        {
            return $"DELETE FROM {nameof(orders)} WHERE {nameof(orders.id)} = @{nameof(orders.id)}";
        }

        public static string GetAll()
        {
            return $@"SELECT o.{nameof(orders.id)}, o.{nameof(orders.payment_method)}, o.{nameof(orders.created_at)},
                              i.{nameof(order_items.id)}, i.{nameof(order_items.amount)}, i.{nameof(order_items.price)}, i.{nameof(order_items.self_price)},
                              m.{nameof(merch_items.id)}, m.{nameof(merch_items.name)},
                              t.{nameof(types.id)}, t.{nameof(types.name)}
                       FROM {nameof(orders)} AS o
                       JOIN {nameof(order_items)} AS i ON o.{nameof(orders.id)} = i.{nameof(order_items.order_id)}
                       JOIN {nameof(merch_items)} AS m ON i.{nameof(order_items.merch_item_id)} = m.{nameof(merch_items.id)}
                       JOIN {nameof(types)} AS t ON m.{nameof(merch_items.type_id)} = t.{nameof(types.id)}";
        }

        public static string GetStatistic(DateTime dateFrom, DateTime dateTo)
        {
            return @$"SELECT items.{nameof(merch_items.name)},
                                      COUNT(o.{nameof(order_items.merch_item_id)}) AS orders_with_item,
	                                  SUM(o.{nameof(order_items.self_price)}) AS total_self_price,
                                      SUM(o.{nameof(order_items.price)}) AS total_price,
                                      SUM(o.{nameof(order_items.amount)}) AS total_amount
                               FROM {nameof(merch_items)} AS items

                               LEFT JOIN {nameof(order_items)} AS o ON o.{nameof(order_items.merch_item_id)} = items.id
                               LEFT JOIN {nameof(orders)} AS ord ON ord.{nameof(orders.id)} = o.{nameof(order_items.order_id)}

                               WHERE ord.created_at BETWEEN '{dateFrom:yyyy-MM-dd}' AND '{dateTo:yyyy-MM-dd}'

                               GROUP BY
                                   items.id

                               ORDER BY items.{nameof(merch_items.name)};

                               SELECT SUM(i.{nameof(order_items.price)}) as absolute_price,
                                      SUM(i.{nameof(order_items.self_price)}) as absolute_self_price,
                                      SUM(i.{nameof(order_items.amount)}) as absolute_amount_sold

                               FROM {nameof(order_items)} as i

                               LEFT JOIN {nameof(orders)} as o on o.{nameof(orders.id)}=i.{nameof(order_items.order_id)}

                               WHERE o.{nameof(orders.created_at)} BETWEEN '{dateFrom:yyyy-MM-dd}' AND '{dateTo:yyyy-MM-dd}';";
        }
    }
}
