using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("types")]
public sealed class Type : ITableEntity<Guid>
{
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}