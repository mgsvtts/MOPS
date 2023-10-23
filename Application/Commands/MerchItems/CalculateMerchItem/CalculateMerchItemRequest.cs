using Domain.MerchItemAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.CalculateMerchItem;
public sealed record CalculateMerchItemRequest(MerchItemId ItemId, int Amount);