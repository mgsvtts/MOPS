namespace Application.Queries.Orders.Statistics;
public readonly record struct GetOrderStatisticQueryResponse(IEnumerable<MerchItemStatistic> Statistics,
                                                    int AbsoluteAmountSold,
                                                    int AbsolutePrice,
                                                    int AbsoluteSelfPrice);

public readonly record struct MerchItemStatistic(string MerchItemName,
                                        int OrdersWithItemCount,
                                        int TotalSelfPrice,
                                        int TotalPrice,
                                        int TotalAmount);
