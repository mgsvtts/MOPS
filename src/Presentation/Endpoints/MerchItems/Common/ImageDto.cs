namespace Presentation.Endpoints.MerchItems.Common;
public record struct ImageDto(Guid Id,
                              bool IsMain,
                              string Url);