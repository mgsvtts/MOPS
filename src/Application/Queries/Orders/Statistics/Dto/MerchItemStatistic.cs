namespace Application.Queries.Orders.Statistics.Dto;

public struct MerchItemStatistic
{
    public string name { get; set; }

    public int orders_with_item { get; set; }

    public int total_self_price { get; set; }

    public int total_price { get; set; }

    public int total_amount { get; set; }
}
