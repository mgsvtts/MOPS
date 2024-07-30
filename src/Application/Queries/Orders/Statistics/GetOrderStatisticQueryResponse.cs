namespace Application.Queries.Orders.Statistics;
public readonly record struct GetOrderStatisticQueryResponse(IEnumerable<MerchItemStatistic> Statistics,
                                                             int AbsoluteAmountSold,
                                                             decimal AbsolutePrice,
                                                             decimal AbsoluteSelfPrice);

public readonly record struct MerchItemStatistic(string MerchItemName,
                                                 int OrdersWithItemCount,
                                                 decimal TotalSelfPrice,
                                                 decimal TotalPrice,
                                                 decimal TotalAmount);