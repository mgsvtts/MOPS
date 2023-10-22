using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models;

[Table("types")]
public class Type
{
    [Column("id")]
    public string Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}
