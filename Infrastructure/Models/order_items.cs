namespace Infrastructure.Models;

public struct order_items
{
    public string id { get; set; }

    public string order_id { get; set; }

    public string merch_item_id { get; set; }

    public int amount { get; set; }

    public decimal price { get; set; }

    public decimal self_price { get; set; }
}
