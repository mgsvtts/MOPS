using Domain.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Orders.GetAllOrders;
public record struct GetAllOrdersQuery() : IRequest<IEnumerable<Order>>;