using Infrastructure.Models;

namespace Infrastructure;

public partial class Queries
{
    public class Image
    {
        public static string Add()
        {
            return @$"INSERT INTO {nameof(images)} ({nameof(images.id)},
                                                    {nameof(images.merch_item_id)},
                                                    {nameof(images.url)},
                                                    {nameof(images.is_main)},
                                                    {nameof(images.public_id)})
                       VALUES (@ImageId,
                               @ItemId,
                               @SecureUrl,
                               @IsMain,
                               @PublicId)";
        }

        public static string Update()
        {
            return @$"UPDATE {nameof(images)} SET
                    {nameof(images.is_main)} = @{nameof(images.is_main)},
                    {nameof(images.merch_item_id)} = @{nameof(images.merch_item_id)},
                    {nameof(images.url)} = @{nameof(images.url)}
                     WHERE {nameof(images.id)} = @{nameof(images.id)}";
        }

        public static string Delete()
        {
            return $"DELETE FROM {nameof(images)} WHERE {nameof(images.id)} = @{nameof(images.id)}";
        }

        public static string GetById()
        {
            return $"SELECT * FROM {nameof(images)} WHERE {nameof(images.id)} = @{nameof(images.id)} LIMIT(1)";
        }
    }
}
