namespace Presentation.Endpoints.MerchItems.Patch.Update;

public sealed record UpdateMerchItemRequest(Guid Id,
                                            Guid? TypeId = null,
                                            string? Name = null,
                                            string? Description = null,
                                            decimal? Price = null,
                                            decimal? SelfPrice = null,
                                            int? AmountLeft = null);