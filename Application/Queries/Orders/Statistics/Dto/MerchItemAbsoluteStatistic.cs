﻿namespace Application.Queries.Orders.Statistics.Dto;

public struct MerchItemAbsoluteStatistic
{
    public int absolute_amount_sold { get; set; }

    public int absolute_price { get; set; }

    public int absolute_self_price { get; set; }
}
