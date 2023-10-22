using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.MerchItems.Create;
public record CreateMerchItemRequest(Guid TypeId,
                                     string Name,
                                     string? Description,
                                     decimal Price,
                                     decimal SelfPrice,
                                     int AmountLeft);
