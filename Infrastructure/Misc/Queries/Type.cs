using Infrastructure.Models;

namespace Infrastructure;

public partial class Queries
{
    public class Type
    {
        public static string GetAll()
        {
            return $"SELECT * FROM {nameof(types)}";
        }

        public static string Add()
        {
            return @$"INSERT INTO {nameof(types)} ({nameof(types.id)},
                                          {nameof(types.name)},
                                          {nameof(types.created_at)})
                       VALUES (@{nameof(types.id)},
                               @{nameof(types.name)},
                               @{nameof(types.created_at)})";
        }

        public static string Delete()
        {
            return $"DELETE FROM {nameof(types)} WHERE id = @{nameof(types.id)}";
        }

        public static string GetById()
        {
            return $"SELECT * FROM {nameof(types)} WHERE id = @{nameof(types.id)}";
        }

        public static string Update()
        {
            return $"UPDATE {nameof(types)} SET {nameof(types.name)} = @{nameof(types.name)}";
        }
    }
}
