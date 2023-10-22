using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models;

[Table("merch_items")]
public sealed class MerchItem
{
    [Column("id")]
    public string Id { get; set; }

    [Column("type_id")]
    public string TypeId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("self_price")]
    public decimal SelfPrice { get; set; }

    [Column("amount")]
    public int Amount { get; set; }
}
