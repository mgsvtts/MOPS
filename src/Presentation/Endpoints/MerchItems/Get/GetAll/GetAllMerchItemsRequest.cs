using Application.Queries.Common;
using Application.Queries.MerchItems.GetAll;

namespace Presentation.Endpoints.MerchItems.Get.GetAll;

public sealed record GetAllMerchItemsRequest(int Page = 1, MerchItemSort Sort = MerchItemSort.NameAsc);