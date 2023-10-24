using Domain.MerchItemAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.ValueObjects;
public sealed record OrderItem(OrderId OrderId,
                               MerchItemId ItemId,
                               MerchItemAmount Amount,
                               MerchItemPrice Price);