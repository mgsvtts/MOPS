using Infrastructure.Common;
using LinqToDB;
using Mediator;

namespace Application.Queries.Orders.Statistics;

public sealed class GetOrderStatisticQueryHandler : IQueryHandler<GetOrderStatisticQuery, GetOrderStatisticQueryResponse>
{
    public async ValueTask<GetOrderStatisticQueryResponse> Handle(GetOrderStatisticQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        var ordersQuery = db.OrderItems
            .Where(x => x.Order.CreatedAt >= request.DateFrom)
            .Where(x => x.Order.CreatedAt <= request.DateTo);

        var absolutePrice = await ordersQuery.SumAsync(x => x.Price, token: cancellationToken);
        var absoluteSelfPrice = await ordersQuery.SumAsync(x => x.SelfPrice, token: cancellationToken);
        var absoluteAmount = await ordersQuery.SumAsync(x => x.Amount, token: cancellationToken);

        var query = from items in db.MerchItems
                    join orderItem in db.OrderItems on items.Id equals orderItem.MerchItemId into orderItemsGroup
                    from orderItem in orderItemsGroup.DefaultIfEmpty()
                    join order in db.Orders on orderItem.OrderId equals order.Id into ordersGroup
                    from order in ordersGroup.DefaultIfEmpty()
                    where order.CreatedAt >= request.DateFrom && order.CreatedAt <= request.DateTo
                    group new { items, orderItem } by new { items.Id, items.Name } into itemGroup
                    select new MerchItemStatistic
                    (
                        itemGroup.Key.Name,
                        itemGroup.Count(x => x.orderItem != null),
                        itemGroup.Sum(x => x.orderItem != null ? x.orderItem.SelfPrice : 0),
                        itemGroup.Sum(x => x.orderItem != null ? x.orderItem.Price : 0),
                        itemGroup.Sum(x => x.orderItem != null ? x.orderItem.Amount : 0)
                    );

        var result = await query.OrderBy(x => x.MerchItemName).ToListAsync(cancellationToken);

        return new GetOrderStatisticQueryResponse(result,
                                                  absoluteAmount,
                                                  absolutePrice,
                                                  absoluteSelfPrice);
    }
}

//return @$"SELECT items.{nameof(merch_items.name)},
//                            COUNT(o.{nameof(order_items.merch_item_id)}) AS orders_with_item,
//	                        SUM(o.{nameof(order_items.self_price)}) AS total_self_price,
//                            SUM(o.{nameof(order_items.price)}) AS total_price,
//                            SUM(o.{nameof(order_items.amount)}) AS total_amount
//                    FROM {nameof(merch_items)} AS items

//                    LEFT JOIN {nameof(order_items)} AS o ON o.{nameof(order_items.merch_item_id)} = items.id
//                    LEFT JOIN {nameof(orders)} AS ord ON ord.{nameof(orders.id)} = o.{nameof(order_items.order_id)}

//                    WHERE ord.created_at BETWEEN '{dateFrom:yyyy-MM-dd}' AND '{dateTo:yyyy-MM-dd}'

//                    GROUP BY
//                        items.id

//                    ORDER BY items.{nameof(merch_items.name)};

//                    SELECT SUM(i.{nameof(order_items.price)}) as absolute_price,
//                            SUM(i.{nameof(order_items.self_price)}) as absolute_self_price,
//                            SUM(i.{nameof(order_items.amount)}) as absolute_amount_sold

//                    FROM {nameof(order_items)} as i

//                    LEFT JOIN {nameof(orders)} as o on o.{nameof(orders.id)}=i.{nameof(order_items.order_id)}

//                    WHERE o.{nameof(orders.created_at)} BETWEEN '{dateFrom:yyyy-MM-dd}' AND '{dateTo:yyyy-MM-dd}';";