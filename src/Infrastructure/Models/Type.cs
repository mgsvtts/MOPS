﻿using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("types")]
public sealed class Type
{
    [PrimaryKey]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}