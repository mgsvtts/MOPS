using Contracts.MerchItem;
using Domain.MerchItemAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.GetAllMerchItems;
public sealed record GetAllMerchItemsResponse(IEnumerable<MerchItemDto> Items);