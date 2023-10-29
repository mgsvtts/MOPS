namespace Infrastructure.Models;

public class merch_items
{
    public string id { get; set; }

    public string type_id { get; set; }

    public types type { get; set; }

    public string name { get; set; }

    public string description { get; set; }

    public decimal price { get; set; }

    public decimal self_price { get; set; }

    public int amount { get; set; }

    public DateTime created_at { get; set; }
}
