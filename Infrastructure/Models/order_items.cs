namespace Infrastructure.Models;

public class order_items
{
    public string id { get; set; }

    public string order_id { get; set; }

    public orders order { get; set; }

    public string merch_item_id { get; set; }

    public merch_items merch_item { get; set; }

    public int amount { get; set; }

    public decimal price { get; set; }

    public decimal self_price { get; set; }
}
