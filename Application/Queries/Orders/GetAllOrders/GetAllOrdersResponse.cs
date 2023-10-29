using Domain.OrderAggregate.ValueObjects;

namespace Application.Queries.Orders.GetAllOrders;
public record struct GetAllOrdersResponse();

public record struct GetAllOrdersResponseOrder(Guid Id,
                                               DateTime CreatedAt,
                                               PaymentMethod PaymentMethod,
                                               IEnumerable<GetAllOrdersResponseOrderItem> Items);

public record struct GetAllOrdersResponseOrderItem(Guid Id,
                                                    decimal Price,
                                                    decimal SelfPrice,
                                                    int Amount,
                                                    GetAllOrdersResponseMerchItem MerchItem);

public record struct GetAllOrdersResponseMerchItem(Guid Id,
                                                   string Name,
                                                   GetAllOrdersResponseType Type);

public record struct GetAllOrdersResponseType(Guid Id,
                                              string Name);
