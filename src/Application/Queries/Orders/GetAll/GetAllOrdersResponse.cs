using Domain.OrderAggregate.ValueObjects;

namespace Application.Queries.Orders.GetAll;

public readonly record struct GetAllOrdersResponseOrder(Guid Id,
                                                        DateTime CreatedAt,
                                                        PaymentMethod PaymentMethod,
                                                        GetAllOrdersResponseTotals Totals,
                                                        IEnumerable<GetAllOrdersResponseOrderItem> Items);

public readonly record struct GetAllOrdersResponseTotals(decimal TotalPrice,
                                                         decimal TotalSelfPrice);

public readonly record struct GetAllOrdersResponseOrderItem(Guid Id,
                                                    decimal Price,
                                                    decimal SelfPrice,
                                                    int Amount,
                                                    GetAllOrdersResponseMerchItem MerchItem);

public readonly record struct GetAllOrdersResponseMerchItem(Guid Id,
                                                   string Name,
                                                   GetAllOrdersResponseType Type);

public readonly record struct GetAllOrdersResponseType(Guid Id,
                                                      string Name);