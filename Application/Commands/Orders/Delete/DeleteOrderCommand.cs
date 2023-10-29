using Domain.OrderAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.Delete;

public record struct DeleteOrderCommand(OrderId Id) : IRequest;
