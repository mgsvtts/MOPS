namespace Application.Queries.Orders.Statistics;
public record struct GetOrderStatisticQueryResponse(IEnumerable<MerchItemStatistic> Statistics,
                                                    int AbsoluteAmountSold,
                                                    int AbsoluteAmountLeft,
                                                    int AbsolutePrice,
                                                    int AbsoluteSelfPrice);

public record struct MerchItemStatistic(string MerchItemName,
                                        int OrdersWithItemCount,
                                        int TotalSelfPrice,
                                        int TotalPrice,
                                        int TotalAmount);
