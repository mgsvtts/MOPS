using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Orders;
public sealed record OrderItem(Guid MerchItemId,
                               int Amount,
                               decimal Price);