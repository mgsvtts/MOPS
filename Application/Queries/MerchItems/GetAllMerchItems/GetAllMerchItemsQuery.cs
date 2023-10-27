using Domain.MerchItemAggregate;
using MediatR;

namespace Application.Queries.MerchItems.GetAllMerchItems;
public record struct GetAllMerchItemsQuery : IRequest<List<MerchItem>>;
