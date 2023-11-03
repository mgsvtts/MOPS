using CloudinaryDotNet.Actions;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Misc.Queries;

internal partial class Queries
{
    internal class Image
    {
        internal static string Add()
        {
            return @$"INSERT INTO {nameof(images)} ({nameof(images.id)},
                                                     {nameof(images.merch_item_id)},
                                                     {nameof(images.url)},
                                                     {nameof(images.is_main)})
                       VALUES (@ImageId,
                               @ItemId,
                               @SecureUrl,
                               @IsMain)";
        }
    }

}