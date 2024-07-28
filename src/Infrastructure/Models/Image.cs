using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("images")]
public sealed class Image
{
    [PrimaryKey]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("merch_item_id")]
    public Guid MerchItemId { get; set; }

    [Column("url")]
    public string Url { get; set; } = null!;

    [Column("public_id")]
    public string PublicId { get; set; } = null!;

    [Column("is_main")]
    public bool IsMain { get; set; }
}