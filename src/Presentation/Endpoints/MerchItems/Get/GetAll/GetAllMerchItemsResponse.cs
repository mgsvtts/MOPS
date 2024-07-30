using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Get.GetAll;
public record struct GetAllMerchItemsResponse(IEnumerable<MerchItemDto> Items);