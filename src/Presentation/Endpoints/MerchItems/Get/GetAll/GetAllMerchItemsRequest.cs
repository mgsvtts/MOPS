using Application.Queries.MerchItems.GetAll;

namespace Presentation.Endpoints.MerchItems.Get.GetAll;

public sealed record GetAllMerchItemsRequest(bool ShowNotAvailable = true,
                                             MerchItemSort Sort = MerchItemSort.NameAsc);