using Infrastructure.Misc.Order;

namespace Infrastructure.Models;

public class Order
{
    public string id { get; set; }

    public string merch_item_id { get; set; }

    public MerchItem Item { get; set; }

    public int amount { get; set; }

    public decimal price { get; set; }

    public PaymentMethod payment_method { get; set; }
}
