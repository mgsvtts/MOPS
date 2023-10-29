using Infrastructure.Misc.Order;

namespace Infrastructure.Models;

public class orders
{
    public string id { get; set; }

    public DateTime created_at { get; set; }

    public PaymentMethod payment_method { get; set; }

    public List<order_items> order_items { get; set; } = new();
}
