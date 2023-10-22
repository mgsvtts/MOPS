using Domain.MerchItemAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.MerchItems.GetAllMerchItems;
public sealed record GetAllMerchItemsQuery : IRequest<List<MerchItem>>;
