using Domain.MerchItemAggregate;
using Infrastructure.Misc.Queries.MerchItems;
using MediatR;

namespace Application.Queries.MerchItems.GetAll;
public record struct GetAllMerchItemsQuery(bool ShowNotAvailable,
                                           MerchItemSort Sort) : IRequest<List<MerchItem>>;
