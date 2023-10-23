﻿namespace Infrastructure.Models;

public sealed class MerchItem
{
    public string id { get; set; }
    public string type_id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public decimal price { get; set; }
    public decimal self_price { get; set; }
    public int amount { get; set; }
}
