using Infrastructure.Misc.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models;

[Table("orders")]
public class Order
{
    [Column("id")]
    public string Id { get; set; }

    [Column("merch_item_id")]
    public string MerchItemId { get; set; }

    public MerchItem Item { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("payment_method")]
    public PaymentMethod PaymentMethod { get; set; }
}
