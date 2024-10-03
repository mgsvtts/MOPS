using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITableEntity<T>
{
    [PrimaryKey]
    [Column("id")]
    public T Id { get; set; }
}