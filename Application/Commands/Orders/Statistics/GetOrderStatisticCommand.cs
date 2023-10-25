using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.Statistics;
public sealed record GetOrderStatisticCommand() : IRequest;