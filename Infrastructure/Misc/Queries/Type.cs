using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Misc.Queries;

internal partial class Queries
{
    internal class Type
    {
        internal static string GetAll()
        {
            return $"SELECT * FROM {nameof(types)}";
        }

        internal static string Add()
        {
            return@$"INSERT INTO {nameof(types)} ({nameof(types.id)},
                                          {nameof(types.name)},
                                          {nameof(types.created_at)})
                       VALUES (@{nameof(types.id)},
                               @{nameof(types.name)},
                               @{nameof(types.created_at)})";
        }

        internal static string Delete()
        {
            return $"DELETE FROM {nameof(types)} WHERE id = @{nameof(types.id)}";
        }

        internal static string GetById()
        {
            return $"SELECT * FROM {nameof(types)} WHERE id = @{nameof(types.id)}";
        }

        internal static string Update()
        {
            return $"UPDATE {nameof(types)} SET {nameof(types.name)} = @{nameof(types.name)}";
        }
    }

}