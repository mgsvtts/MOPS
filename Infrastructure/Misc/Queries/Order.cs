using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Misc.Queries;
internal partial class Queries
{
    internal static class Order
    {
        internal static string AddOrder()
        {
            return $@"INSERT INTO {nameof(orders)} ({nameof(orders.id)},
                                                 {nameof(orders.created_at)},
                                                 {nameof(orders.payment_method)})
                             VALUES (@{nameof(orders.id)},
                                     @{nameof(orders.created_at)},
                                     @{nameof(orders.payment_method)})";
        }

        internal static string AddOrderItems()
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

        internal static string GetById()
        {
            return $"SELECT * FROM {nameof(orders)} WHERE id = @{nameof(orders.id)} LIMIT (1)";
        }

        internal static string Delete()
        {
            return $"DELETE FROM {nameof(orders)} WHERE {nameof(orders.id)} = @{nameof(orders.id)}";
        }
    }
}