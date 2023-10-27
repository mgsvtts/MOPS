using Infrastructure.Misc.Order;

namespace Infrastructure.Models;

public struct orders
{
    public string id { get; set; }

    public DateTime created_at { get; set; }

    public PaymentMethod payment_method { get; set; }
}
