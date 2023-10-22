using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities.ValueObjects.Types;
using Domain.MerchItemAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.MerchItem;

public sealed record MerchItemDto(Guid Id,
                                  Guid TypeId,
                                  string Name,
                                  string? Description,
                                  decimal Price,
                                  decimal SelfPrice,
                                  int AmountLeft);
