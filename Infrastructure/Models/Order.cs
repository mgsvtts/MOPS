using Infrastructure.Misc.Order;

namespace Infrastructure.Models;

public class Order
{
    public string id { get; set; }

    public DateTime created_at { get; set; }

    public PaymentMethod payment_method { get; set; }
}
