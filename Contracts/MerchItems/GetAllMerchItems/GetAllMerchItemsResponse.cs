using Contracts.MerchItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.MerchItems.GetAllMerchItems;
public sealed record GetAllMerchItemsResponse(IEnumerable<MerchItemDto> Items);