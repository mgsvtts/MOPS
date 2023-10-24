using Infrastructure.Misc.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models;

public class OrderItem
{
    public string id { get; set; }
    public string order_id { get; set; }
    public string merch_item_id { get; set; }
    public int amount { get; set; }
    public decimal price { get; set; }
}
