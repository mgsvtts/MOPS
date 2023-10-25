using Domain.OrderAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Orders.Create;
public sealed record CreateOrderRequest(IEnumerable<OrderItemRequest> Items,
                                        PaymentMethod PaymentMethod);