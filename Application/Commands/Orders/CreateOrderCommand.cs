using Domain.OrderAggregate;
using Domain.OrderAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders;
public sealed record CreateOrderCommand(IEnumerable<OrderItem> Items,
                                        PaymentMethod PaymentMethod) :IRequest<Order>;
